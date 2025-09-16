using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IPasswordHash passwordHash) : ICommandHandler<CreateUserCommand, ResultT<UserResponse>>
    {
        public async Task<ResultT<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var loggedRole = currentUserService.Role;
            var loggedTenantId = currentUserService.TenantId;

            Guid tenantIdToUse;

            if (loggedRole == UserRoleEnum.AdminSystem)
            {
                if (request.TenantId is null)
                    return TenantErrors.TenantRequired();

                tenantIdToUse = request.TenantId.Value;
                var tenantExist = await unitOfWork.Tenants.GetByIdAsync(TenantId.Of(tenantIdToUse));
                if (tenantExist is null)
                    return TenantErrors.NotFound(tenantIdToUse);
            }
            else
            {
                tenantIdToUse = loggedTenantId!.Value;

                if (request.Role == UserRoleEnum.AdminSystem)
                    return UserErrors.Forbidden();
            }

            var userExist = await unitOfWork.Users.FirstOrDefaultAsync(new GetUserByEmailGlobalSpecification(request.Email));
            if (userExist != null)
                return UserErrors.Conflict(request.Email);

            
            var user = User.Create(
                request.Name,
                Contact.Of(request.Email, request.PhoneNumber),
                request.Password,
                request.Role,
                TenantId.Of(tenantIdToUse),
                passwordHash
            );

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.CompleteAsync();

            return user.ToDto();
        }
    }
}
