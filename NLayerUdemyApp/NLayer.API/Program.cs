using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.API.Modules;
using NLayer.Repository;
using NLayer.Service.Mapping;
using NLayer.Service.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()));
builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MapProfile)));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        options => { options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))?.GetName().Name); });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomException();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();