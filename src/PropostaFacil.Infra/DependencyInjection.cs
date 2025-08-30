using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Clients;
using PropostaFacil.Application.Proposals;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Application.Users;
using PropostaFacil.Infra.Data;
using PropostaFacil.Infra.Data.Interceptors;
using PropostaFacil.Infra.Data.Repositories;
using PropostaFacil.Infra.Services;
using PropostaFacil.Shared.Messaging.MassTransit;
using System.Reflection;

namespace PropostaFacil.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            var connectionString = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<PropostaFacilDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            //Repositories
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IProposalRepository, ProposalRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenCleanupService, TokenCleanupService>();
            services.AddScoped<IEmailSender, SendGridEmailSender>();
            services.AddScoped<IAsaasService, AsaasService>();
            services.AddScoped<IRedisCacheService, RedisCacheService>();

            return services;
        }
    }
}
