using PropostaFacil.Application.Contracts.Data;
using PropostaFacil.Infra.Data;
using PropostaFacil.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PropostaFacil.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (this IServiceCollection services, IConfiguration configuration)
        {


            var connectionString = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<PropostaFacilDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString);
            });

            //Repositories
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITenantRepository, TenantRepository>();

            return services;
        }
    }
}
