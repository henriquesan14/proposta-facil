using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.RefreshTokens;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using System.Linq.Expressions;

namespace PropostaFacil.Application.Auth.Commands.GenerateAccessToken
{
    public class GenerateAccessTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, ICurrentUserService currentUserService, IPasswordCheck passwordCheck) : ICommandHandler<GenerateAccessTokenCommand, ResultT<AuthResponse>>
    {
        public async Task<ResultT<AuthResponse>> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> predicate = u => u.Contact.Email == request.Email;
            var user = await unitOfWork.Users.FirstOrDefaultAsync(new GetUserByEmailGlobalSpecification(request.Email));
            if (user == null)
                return AuthErrors.Unauthorized();
            bool password = user.CanUserLogin(request.Password, passwordCheck);
            if (!password)
            {
                return AuthErrors.Unauthorized();
            }
            var authToken = tokenService.GenerateAccessToken(user);

            var refreshToken = RefreshToken.Create(
                id: RefreshTokenId.Of(Guid.NewGuid()),
                token: authToken.RefreshToken,
                userId: UserId.Of(user.Id.Value),
                expiresAt: authToken.RefreshTokenExpiresAt,
                createdByIp: currentUserService.IpAddress!
            );
            await unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await unitOfWork.CompleteAsync();

            currentUserService.SetCookieTokens(authToken.AccessToken, authToken.RefreshToken);

            var authResponse = new AuthResponse(user.Id.Value, user.Name, user.Role);
            return authResponse;
        }
    }
}
