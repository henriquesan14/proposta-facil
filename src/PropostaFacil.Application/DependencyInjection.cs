using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;

namespace PropostaFacil.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });

            services.AddValidatorsFromAssemblyContaining<CreateTenantCommandValidator>();

            return services;
        }
    }
}
