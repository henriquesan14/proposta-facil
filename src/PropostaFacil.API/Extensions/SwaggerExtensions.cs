using Microsoft.OpenApi.Models;

namespace PropostaFacil.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PropostaFacil API",
                    Version = "v1"
                });
            });



            return services;
        }

        public static WebApplication UseSwaggerDocs(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PropostaFacil API V1");
                });
            }

            return app;
        }
    }
}
