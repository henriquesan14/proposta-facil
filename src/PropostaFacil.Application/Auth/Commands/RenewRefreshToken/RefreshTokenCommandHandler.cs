using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Users;
using PropostaFacil.Domain.RefreshTokens;
using PropostaFacil.Domain.RefreshTokens.Specifications;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.RenewRefreshToken;

public class RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, ITokenService tokenService) : ICommandHandler<RefreshTokenCommand, ResultT<AuthResponse>>
{
    public async Task<ResultT<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = userContext.RefreshToken;
        var existingToken = await unitOfWork.RefreshTokens
            .SingleOrDefaultAsync(new GetRefreshTokenByTokenSpecification(refreshToken!));

        if (existingToken is null || existingToken.IsExpired || existingToken.IsRevoked)
        {
            return AuthErrors.SessionExpired();
        }

        var user = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserByIdGlobalSpecification(existingToken.UserId));
        if (user is null)
        {
            return UserErrors.NotFound(existingToken.UserId.Value);
        }

        var authToken = tokenService.GenerateAccessToken(user);
        var newRefreshToken = RefreshToken.Create(RefreshTokenId.Of(Guid.NewGuid()), authToken.RefreshToken, user.Id, userContext.IpAddress!, DateTime.Now.AddDays(7));

        existingToken.Revoke(userContext.IpAddress!);
        existingToken.SetReplacedByToken(newRefreshToken.Token);


        await unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
        await unitOfWork.CompleteAsync();

        userContext.SetCookieTokens(authToken.AccessToken, authToken.RefreshToken);

        var authResponse = new AuthResponse(user.Id.Value, user.Name, user.Role);
        return authResponse;
    }
}
