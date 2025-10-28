using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.RefreshTokens.Specifications;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.RevokeRefreshToken;

public class RevokeRefreshTokenCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<RevokeRefreshTokenCommand, Result>
{
    public async Task<Result> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = userContext.RefreshToken;
        if (string.IsNullOrEmpty(refreshToken))
            return AuthErrors.RefreshTokenNotFound();

        var token = await unitOfWork.RefreshTokens.SingleOrDefaultAsync(new GetRefreshTokenByTokenSpecification(refreshToken));
        if (token == null)
            return AuthErrors.RefreshTokenNotFound();

        token.Revoke(userContext.IpAddress!);
        await unitOfWork.CompleteAsync();

        userContext.RemoveCookiesToken();

        return Result.Success();
    }
}
