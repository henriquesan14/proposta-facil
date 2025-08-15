using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropostaFacil.Application.Auth;
using PropostaFacil.Application.Clients;
using PropostaFacil.Application.Proposals;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Application.Users;
using PropostaFacil.Infra.Data;
using PropostaFacil.Infra.Data.Interceptors;
using PropostaFacil.Infra.Data.Repositories;
using PropostaFacil.Infra.Services;

namespace PropostaFacil.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
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

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenCleanupService, TokenCleanupService>();

            return services;
        }
    }
}
