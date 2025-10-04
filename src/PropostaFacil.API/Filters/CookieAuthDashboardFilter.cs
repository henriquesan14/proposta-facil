using Hangfire.Dashboard;
using PropostaFacil.Domain.Enums;
using System.Security.Claims;

namespace PropostaFacil.API.Filters;

public class CookieAuthDashboardFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        if (httpContext.User.Identity?.IsAuthenticated != true)
            return false;

        return httpContext.User.HasClaim(ClaimTypes.Role, UserRoleEnum.AdminSystem.ToString());
    }
}
