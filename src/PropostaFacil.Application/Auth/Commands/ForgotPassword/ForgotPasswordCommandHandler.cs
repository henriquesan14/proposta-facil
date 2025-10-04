using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.ForgotPassword;

internal class ForgotPasswordCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<ForgotPasswordCommand, Result>
{
    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserByEmailGlobalSpecification(request.Email));
        if (user is null) return AuthErrors.UserEmailNotFound(request.Email);

        user.ForgotPassword();

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
