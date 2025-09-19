using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IPasswordHash passwordHash, IUserRuleCheck userRuleCheck) : ICommandHandler<CreateUserCommand, ResultT<UserResponse>>
    {
        public async Task<ResultT<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var loggedTenantId = TenantId.Of(currentUserService.TenantId!.Value);

            if (request.Role == UserRoleEnum.AdminSystem)
                return UserErrors.Forbidden();

            var user = User.Create(
                request.Name,
                Contact.Of(request.Email, request.PhoneNumber),
                request.Password,
                request.Role,
                loggedTenantId,
                passwordHash,
                userRuleCheck
            );

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.CompleteAsync();

            return user.ToDto();
        }
    }
}
