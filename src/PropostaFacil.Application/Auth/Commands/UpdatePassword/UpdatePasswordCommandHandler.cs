using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Users;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.UpdatePassword;

public class UpdatePasswordCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IPasswordCheck passwordCheck, IPasswordHash passwordHash) : ICommandHandler<UpdatePasswordCommand, Result>
{
    public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {

        var userId = UserId.Of(currentUserService.UserId!.Value);
        var user = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserByIdGlobalSpecification(userId), cancellationToken);
        if (user == null)return UserErrors.NotFound(currentUserService.UserId!.Value);

        user!.ChangePassword(request.OldPassword, request.NewPassword, passwordCheck, passwordHash);
        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
