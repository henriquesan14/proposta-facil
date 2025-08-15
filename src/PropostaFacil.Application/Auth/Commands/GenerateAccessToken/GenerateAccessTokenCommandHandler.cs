using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;
using System.Linq.Expressions;

namespace PropostaFacil.Application.Auth.Commands.GenerateAccessToken
{
    public class GenerateAccessTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, ICurrentUserService currentUserService) : ICommandHandler<GenerateAccessTokenCommand, ResultT<AuthResponse>>
    {
        public async Task<ResultT<AuthResponse>> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> predicate = u => u.Contact.Email == request.Email;
            var user = await unitOfWork.Users.GetSingleAsync(predicate);
            if (user == null)
                return AuthErrors.Unauthorized();
            bool password = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
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

            var authResponse = new AuthResponse(user.Id.Value, user.Name, user.Role, user.TenantId.Value);
            return ResultT<AuthResponse>.Success(authResponse);
        }
    }
}
