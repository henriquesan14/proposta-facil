using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Commands.AdminCreateUser;

public class AdminCreateUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHash passwordHash, IUserRuleCheck userRuleCheck) : ICommandHandler<AdminCreateUserCommand, ResultT<UserResponse>>
{
    public async Task<ResultT<UserResponse>> Handle(AdminCreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            request.Name,
            Contact.Of(request.Email, request.PhoneNumber),
            request.Role,
            TenantId.Of(request.TenantId),
            passwordHash,
            userRuleCheck
        );

        await unitOfWork.Users.AddAsync(user);
        await unitOfWork.CompleteAsync();

        return user.ToDto();
    }
}
