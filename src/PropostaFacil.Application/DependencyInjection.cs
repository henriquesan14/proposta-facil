using Common.ResultPattern;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropostaFacil.Application.Subscriptions;
using PropostaFacil.Application.Subscriptions.Queries.GetSubscriptionPlans;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;
using StackExchange.Redis;

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

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Host"];
                options.InstanceName = configuration["Redis:InstanceName"];
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(configuration["Redis:Host"]!);
            });

            services.AddScoped<IQueryHandler<GetSubscriptionPlansQuery, ResultT<PaginatedResult<SubscriptionPlanResponse>>>, GetSubscriptionPlansQueryHandler>();
            services.Decorate<IQueryHandler<GetSubscriptionPlansQuery, ResultT<PaginatedResult<SubscriptionPlanResponse>>>, CachedGetSubscriptionPlansQueryHandler>();

            return services;
        }
    }
}
