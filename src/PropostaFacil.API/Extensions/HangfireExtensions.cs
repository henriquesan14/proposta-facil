using Hangfire;
using Hangfire.PostgreSql;
using PropostaFacil.API.Filters;
using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Shared.Interfaces;

namespace PropostaFacil.API.Extensions;

public static class HangfireExtensions
{
    public static IServiceCollection AddHangfireConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");

        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString)));

        services.AddHangfireServer();

        return services;
    }


    public static IApplicationBuilder UseHangfireDashboardWithAuth(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new CookieAuthDashboardFilter() }
        });

        return app;
    }

    public static IApplicationBuilder UseRecurringJobs(this IApplicationBuilder app)
    {
        RecurringJob.AddOrUpdate<ITokenCleanupService>(
            "CleanupExpiredAndRevokedTokensAsync",
            service => service.CleanupExpiredAndRevokedTokensAsync(),
            Cron.Daily);

        RecurringJob.AddOrUpdate<ISubscriptionsJobService>(
            "CheckOverduePayments",
            service => service.CheckOverduePayments(),
             Cron.Daily);


        return app;
    }
}
