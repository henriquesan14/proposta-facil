using AspNetCoreRateLimit;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PropostaFacil.API.ErrorHandling;
using PropostaFacil.API.Extensions;

namespace PropostaFacil.API
{
    public static class DependencyInjection
    {
        public static void ConfigureHostUrls(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsProduction())
            {
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
                builder.WebHost.UseUrls($"http://*:{port}");
            }
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services, WebApplicationBuilder builder, IConfiguration configuration)
        {
            services.AddSwaggerConfig();
            services.AddCorsConfig(builder.Environment);
            services.AddJsonSerializationConfig();
            services.AddAuthConfig(configuration, builder.Environment);

            services.AddHangfireConfig(configuration);

            builder.Services.AddControllers()
             .ConfigureApiBehaviorOptions(options =>
             {
                 options.SuppressModelStateInvalidFilter = true;
             });

            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DbConnection")!);

            services.AddRateLimitingConfig(builder.Configuration);

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app, IConfiguration configuration)
        {
            app.UseExceptionHandler(options => { });

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseSwaggerDocs();

            app.UseIpRateLimiting();

            app.UseHangfireDashboardWithAuth(configuration);
            app.UseRecurringJobs();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}
