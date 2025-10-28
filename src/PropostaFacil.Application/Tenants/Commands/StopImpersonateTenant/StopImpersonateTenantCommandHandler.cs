using Common.ResultPattern;
using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.RefreshTokens;
using PropostaFacil.Domain.RefreshTokens.Specifications;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.StopImpersonateTenant;

public class StopImpersonateTenantCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUserContext userContext) : ICommandHandler<StopImpersonateTenantCommand, ResultT<AuthResponse>>
{
    public async Task<ResultT<AuthResponse>> Handle(StopImpersonateTenantCommand request, CancellationToken cancellationToken)
    {
        var currentRefreshToken = userContext.RefreshToken;
        if (string.IsNullOrEmpty(currentRefreshToken)) return AuthErrors.RefreshTokenNotFound();

        var userAdminSystem = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserAdminSystemSpecification());
        if (userAdminSystem == null) return AuthErrors.Unauthorized();

        var token = await unitOfWork.RefreshTokens.SingleOrDefaultAsync(new GetRefreshTokenByTokenSpecification(currentRefreshToken));
        if (token == null)
            return AuthErrors.RefreshTokenNotFound();

        token.Revoke(userContext.IpAddress!);

        var authToken = tokenService.GenerateAccessToken(userAdminSystem!);

        var refreshToken = RefreshToken.Create(
            id: RefreshTokenId.Of(Guid.NewGuid()),
            token: authToken.RefreshToken,
            userId: UserId.Of(userAdminSystem.Id.Value),
            expiresAt: authToken.RefreshTokenExpiresAt,
            createdByIp: userContext.IpAddress!
        );

        await unitOfWork.RefreshTokens.AddAsync(refreshToken);
        await unitOfWork.CompleteAsync();

        userContext.SetCookieTokens(authToken.AccessToken, authToken.RefreshToken);

        var authResponse = new AuthResponse(userAdminSystem.Id.Value, userAdminSystem.Name, Domain.Enums.UserRoleEnum.AdminSystem);
        return authResponse;
    }
}
