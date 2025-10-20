namespace PropostaFacil.Application.Auth;

public interface ITokenCleanupService
{
    Task CleanupExpiredAndRevokedTokensAsync();
}
