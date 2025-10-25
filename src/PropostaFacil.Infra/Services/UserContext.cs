using Microsoft.AspNetCore.Http;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Users.Contracts;
using System.Security.Claims;

namespace PropostaFacil.Infra.Services;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public Guid? UserId => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value is string id
        ? Guid.Parse(id)
        : null;

    public string? UserName => User?.FindFirst("name")?.Value;

    public Guid? TenantId
    {
        get
        {
            // Se estiver impersonando, usa o tenant impersonado
            var impersonatedTenantId = User?.FindFirst("impersonate_tenant_id")?.Value;
            if (!string.IsNullOrEmpty(impersonatedTenantId))
                return Guid.Parse(impersonatedTenantId);

            // Caso contrário, usa o tenant normal do usuário
            var tenantId = User?.FindFirst("tenant_id")?.Value;
            return !string.IsNullOrEmpty(tenantId) ? Guid.Parse(tenantId) : null;
        }
    }

    public UserRoleEnum? Role => User?.FindFirst(ClaimTypes.Role)?.Value is string role
        ? Enum.TryParse<UserRoleEnum>(role, out var parsed) ? parsed : null
        : null;

    public bool IsAdminSystem => Role == UserRoleEnum.AdminSystem;

    public string? IpAddress
    {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return null;

            if (httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                return forwardedFor.FirstOrDefault();
            }

            return httpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }

    public string? RefreshToken
    {
        get
        {
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refresh_token"];
            return refreshToken;
        }
    }

    public void SetCookieTokens(string accessToken, string refreshToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            return;

        httpContext.Response.Cookies.Append("access_token", accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddHours(1)
        });

        httpContext.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(7)
        });
    }

    public void RemoveCookiesToken()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refresh_token", new CookieOptions
        {
            Path = "/",
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None
        });

        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("access_token", new CookieOptions
        {
            Path = "/",
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None
        });
    }
}
