using Common.ResultPattern;
using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.RefreshTokens;
using PropostaFacil.Domain.Tenants.Specifications;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Tenants.Commands.ImpersonateTenant;

public class ImpersonateTenantCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUserContext currentUserService) : ICommandHandler<ImpersonateTenantCommand, ResultT<AuthResponse>>
{
    public async Task<ResultT<AuthResponse>> Handle(ImpersonateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await unitOfWork.Tenants.SingleOrDefaultAsync(new GetTenantByIdGlobalSpecification(TenantId.Of(request.TenantId)));
        if(tenant == null) return TenantErrors.NotFound(request.TenantId);

        var userAdminSystem = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserAdminSystemSpecification());
        if (userAdminSystem == null) return AuthErrors.Unauthorized();

        var authToken = tokenService.GenerateAccessToken(userAdminSystem!, request.TenantId);

        var refreshToken = RefreshToken.Create(
            id: RefreshTokenId.Of(Guid.NewGuid()),
            token: authToken.RefreshToken,
            userId: UserId.Of(userAdminSystem.Id.Value),
            expiresAt: authToken.RefreshTokenExpiresAt,
            createdByIp: currentUserService.IpAddress!
        );

        await unitOfWork.RefreshTokens.AddAsync(refreshToken);
        await unitOfWork.CompleteAsync();

        currentUserService.SetCookieTokens(authToken.AccessToken, authToken.RefreshToken);

        var authResponse = new AuthResponse(userAdminSystem.Id.Value, userAdminSystem.Name, Domain.Enums.UserRoleEnum.AdminTenant, tenant.ToDto());
        return authResponse;
    }
}
