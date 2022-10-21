using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using servicios_identificacion.Cors.Persistence;
using Trabajadores.Cors.Entity;
using Trabajadores.Cors.Domain;
using MediatR;
using FluentValidation.AspNetCore;
using Trabajadores.Cors.Domain.Helper;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using AuthenticacionManger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Registro>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//database context
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

//var dbHost = "localhost";
//var dbName = "Seguridad";
//var dbPassword = "1Arrepientete97@";

var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User id=sa;Password={dbPassword}";
builder.Services.AddDbContext<SeguridadContexto>(opt=>opt.UseSqlServer(connectionString));
///
var builderA = builder.Services.AddIdentityCore<Usuario>();
var identityBuilder = new IdentityBuilder(builderA.UserType, builderA.Services);
identityBuilder.AddEntityFrameworkStores<SeguridadContexto>();
identityBuilder.AddSignInManager<SignInManager<Usuario>>();
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();
builder.Services.AddAutoMapper(typeof(Registro.UsuarioRegistroHandler));
builder.Services.AddMediatR(typeof(Registro.UsuarioRegistroCommand).Assembly);
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddSingleton<JwTokenHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
