namespace PropostaFacil.Application.Shared.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        Guid? TenantId { get; }
        string Role { get; }

        bool IsAdminSystem { get; }
        string? IpAddress { get; }
        string? RefreshToken { get; }
        void SetCookieTokens(string accessToken, string refreshToken);
        void RemoveCookiesToken();
    }
}
