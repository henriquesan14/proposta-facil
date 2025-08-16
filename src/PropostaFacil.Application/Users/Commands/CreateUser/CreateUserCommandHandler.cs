using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<CreateUserCommand, ResultT<UserResponse>>
    {
        public async Task<ResultT<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var loggedRole = currentUserService.Role;
            var loggedTenantId = currentUserService.TenantId;

            Guid tenantIdToUse;

            if (loggedRole == UserRoleEnum.AdminTenant)
            {
                tenantIdToUse = loggedTenantId!.Value;

                if (request.Role == UserRoleEnum.AdminSystem)
                    return UserErrors.Forbidden();
            }
            else if (loggedRole == UserRoleEnum.AdminSystem)
            {
                if (request.TenantId is null)
                    return UserErrors.TenantRequired();

                tenantIdToUse = request.TenantId.Value;
            }
            else
            {
                return UserErrors.Forbidden();
            }

            var tenantExist = await unitOfWork.Tenants.GetByIdAsync(TenantId.Of(tenantIdToUse));
            if (tenantExist is null)
                return UserErrors.NotFound(tenantIdToUse);

            var userExist = await unitOfWork.Users.GetSingleAsync(u => u.Contact.Email == request.Email);
            if (userExist != null)
                return UserErrors.Conflict(request.Email);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, 8);
            var user = User.Create(
                request.Name,
                Contact.Of(request.Email, request.PhoneNumber),
                passwordHash,
                request.Role,
                TenantId.Of(tenantIdToUse)
            );

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.CompleteAsync();

            return user.ToDto();
        }
    }
}
