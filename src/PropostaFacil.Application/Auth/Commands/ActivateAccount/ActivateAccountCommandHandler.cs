using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.ActivateAccount;

public class ActivateAccountCommandHandler(IUnitOfWork unitOfWork, IPasswordHash passwordHasher)
    : ICommandHandler<ActivateAccountCommand, Result>
{
    public async Task<Result> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserByVerifiedTokenGlobalSpecification(request.Token));

        if (request.Token is null || user is null) return AuthErrors.InvalidVerifiedToken();

        user.VerifyAndActivate(request.Token, passwordHasher.HashPassword(request.Password));

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
