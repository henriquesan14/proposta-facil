using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IUserContext currentUserService) : ICommandHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var loggedTenantId = TenantId.Of(currentUserService.TenantId!.Value);

        if (request.Role == UserRoleEnum.AdminSystem)
            return UserErrors.Forbidden();

        var userId = UserId.Of(request.Id);
        var user = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserByIdSpecification(userId));
        if (user == null) return UserErrors.NotFound(request.Id);

        var userExist = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserByEmailGlobalSpecification(request.Email));
        if (userExist != null && userExist.Id != userId) return UserErrors.Conflict(request.Email);

        var contact = Contact.Of(request.Email, request.PhoneNumber);

        user.Update(request.Name, contact, request.Role);

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
