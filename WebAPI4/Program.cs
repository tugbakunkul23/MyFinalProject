using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Controllers + FluentValidation
builder.Services.AddControllers();
builder.Services.AddScoped<IValidator<Product>, ProductValidator>();



builder.Services.AddDbContext<NorthwindContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



//builder.Services.AddDbContext<NorthwindContext>(options =>
//    options.UseSqlServer(@"Server=DESKTOP-86OTNL5\SQLEXPRESS;Database=NORTHWIND;Trusted_Connection=true;TrustServerCertificate=True")
//    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Transient);



// Autofac'i ASP.NET Core'a bildir
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Autofac modüllerini ekle
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});

//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// JWT Token ayarlarýný appsettings.json'dan oku
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

// Authentication ekle
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

//builder.Services.AddDependencyResolvers();
builder.Services.AddDependencyResolvers(new ICoreModule[] {
    new CoreModule()
});



// Add services to the container.

//Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject-->IoC container
//AOP
builder.Services.AddControllers();
// Dependency Injection kayýtlarý
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Middleware -sýraya koyuyorsun middleware budur.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication(); // kimlik doðrulama middleware'i

app.UseAuthorization();

app.MapControllers();

app.Run();
