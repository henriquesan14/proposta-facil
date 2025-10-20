using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Users;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.ResetPassword;

public class ResetPasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordHash passwordHash) : ICommandHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserByIdGlobalSpecification(UserId.Of(request.UserId)));
        if (user == null) return UserErrors.NotFound(request.UserId);

        user.ResetPassword(request.Token, request.Password, passwordHash);

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
