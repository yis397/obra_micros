using ApiGateway.Handler;
using ApiGateway.Models;
using AuthenticacionManger.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration).AddDelegatingHandler<BasicHandler>(); ;
builder.Services.AddSingleton<RespuetaHandler>();

builder.Services.AddHttpClient("TrabajadorService", config =>
{
    config.BaseAddress = new Uri(Environment.GetEnvironmentVariable("URL_TRA"));
});
builder.Services.AddCustomJwtAuth();
var app = builder.Build();
await app.UseOcelot();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
