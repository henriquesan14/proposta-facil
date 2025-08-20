using PropostaFacil.API;
using PropostaFacil.Application;
using PropostaFacil.Infra;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddInfrastructure(configuration)
    .AddApplication(configuration)
    .AddApiServices(builder, configuration);


var app = builder.Build();

app.UseApiServices(configuration);

await app.RunAsync();
