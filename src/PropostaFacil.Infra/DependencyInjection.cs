using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropostaFacil.Application.Clients;
using PropostaFacil.Application.Proposals;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Infra.Data;
using PropostaFacil.Infra.Data.Interceptors;
using PropostaFacil.Infra.Data.Repositories;

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

            return services;
        }
    }
}
