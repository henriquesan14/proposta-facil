using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Users;
using PropostaFacil.Domain.RefreshTokens;
using PropostaFacil.Domain.RefreshTokens.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.RenewRefreshToken;

public class RefreshTokenCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, ITokenService tokenService) : ICommandHandler<RefreshTokenCommand, ResultT<AuthResponse>>
{
    public async Task<ResultT<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = currentUserService.RefreshToken;
        var existingToken = await unitOfWork.RefreshTokens
            .SingleOrDefaultAsync(new GetRefreshTokenByTokenSpecification(refreshToken!));

        if (existingToken is null || existingToken.IsExpired || existingToken.IsRevoked)
        {
            return AuthErrors.SessionExpired();
        }

        var user = await unitOfWork.Users.GetByIdAsync(existingToken.UserId);
        if (user is null)
        {
            return UserErrors.NotFound(existingToken.UserId.Value);
        }

        var authToken = tokenService.GenerateAccessToken(user);
        var newRefreshToken = RefreshToken.Create(RefreshTokenId.Of(Guid.NewGuid()), authToken.RefreshToken, user.Id, currentUserService.IpAddress!, DateTime.Now.AddDays(7));

        existingToken.Revoke(currentUserService.IpAddress!);
        existingToken.SetReplacedByToken(newRefreshToken.Token);


        await unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
        await unitOfWork.CompleteAsync();

        currentUserService.SetCookieTokens(authToken.AccessToken, authToken.RefreshToken);

        var authResponse = new AuthResponse(user.Id.Value, user.Name, user.Role);
        return authResponse;
    }
}
