using Common.ResultPattern;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionPlans;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });

            services.AddValidatorsFromAssemblyContaining<CreateTenantCommandValidator>();

            services.AddScoped<IQueryHandler<GetSubscriptionPlansQuery, ResultT<IEnumerable<SubscriptionPlanResponse>>>, GetSubscriptionPlansQueryHandler>();
            services.Decorate<IQueryHandler<GetSubscriptionPlansQuery, ResultT<IEnumerable<SubscriptionPlanResponse>>>, CachedGetSubscriptionPlansQueryHandler>();

            return services;
        }
    }
}
