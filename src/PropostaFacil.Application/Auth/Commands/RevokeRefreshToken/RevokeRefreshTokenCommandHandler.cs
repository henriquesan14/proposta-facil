using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.RefreshTokens.Specifications;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Auth.Commands.RevokeRefreshToken;

public class RevokeRefreshTokenCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<RevokeRefreshTokenCommand, Result>
{
    public async Task<Result> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = currentUserService.RefreshToken;
        if (string.IsNullOrEmpty(refreshToken))
            return AuthErrors.RefreshTokenNotFound();

        var token = await unitOfWork.RefreshTokens.SingleOrDefaultAsync(new GetRefreshTokenByTokenSpecification(refreshToken));
        if (token == null)
            return AuthErrors.RefreshTokenNotFound();

        token.Revoke(currentUserService.IpAddress!);
        await unitOfWork.CompleteAsync();

        currentUserService.RemoveCookiesToken();

        return Result.Success();
    }
}
