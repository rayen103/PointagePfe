using System.IO.Abstractions;
using System.Text.Json;
using CollectManagement.Application.Common;
using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Authentification;
using CollectManagement.Application.Interfaces.Repositories;
using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;
using CollectManagement.Application.Interfaces.Repositories.Employes;
using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Application.Interfaces.Repositories.Equipes;
using CollectManagement.Application.Interfaces.Repositories.OrdresTravail;
using CollectManagement.Application.Interfaces.Repositories.OrdresTravailDetails;
using CollectManagement.Application.Interfaces.Repositories.Shifts;
using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Application.Interfaces.Repositories.Societes;
using CollectManagement.Application.Interfaces.Repositories.Utilisateurs;
using CollectManagement.Application.Interfaces.Services;
using CollectManagement.Application.Shared;
using CollectManagement.Infrastructure.Authentification;
using CollectManagement.Infrastructure.Common;
using CollectManagement.Infrastructure.Interceptors;
using CollectManagement.Infrastructure.Persistence;
using CollectManagement.Infrastructure.Persistence.Context;
using CollectManagement.Infrastructure.Persistence.Repositories;
using CollectManagement.Infrastructure.Persistence.Repositories.BusRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.CircuitRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.CircuitPointCollecteRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.EmployeRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.EquipeRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.OrdreTravailRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.OrdreTravailDetailRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.ShiftRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.PointCollecteRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.RattachementRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.SocieteRepositories;
using CollectManagement.Infrastructure.Persistence.Repositories.UtilisateurRepositories;
using CollectManagement.Infrastructure.PuppeteerConfig;
using CollectManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CollectManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ILoggedInUserService, LoggedInUserService>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.Configure<PuppeteerOptions>(configuration.GetSection(PuppeteerOptions.SectionName));
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new NullableUlidJsonConverter());
        });

        services.AddScoped<AuditableInterceptor>();
        
        services
            .AddAuth(configuration)
            .AddAuthorization()
            .AddDbContext(configuration)
            .AddPersistance();

        
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IFileSystem, FileSystem>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
        services.AddScoped<IBrowserProvider, BrowserProvider>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddHttpContextAccessor();
        
        
        return services;
    }
    
    public static IServiceCollection AddDbContext(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServerConnection");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
            options
                //.UseLazyLoadingProxies()
                .UseSqlServer(
                    connectionString,
                    o =>
                        o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "F3SManagement")
                            .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .AddInterceptors(sp.GetRequiredService<AuditableInterceptor>())
                .EnableSensitiveDataLogging()
            );
        
        return services;
    }
    
  

    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();
        services.AddScoped<ISocieteRepository, SocieteRepository>();
        services.AddScoped<IRoleUtilisateurRepository, RoleUtilisateurRepository>();
        services.AddScoped<IEmployeRepository, EmployeRepository>();
        services.AddScoped<ICircuitRepository, CircuitRepository>();
        services.AddScoped<ICircuitPointCollecteRepository, CircuitPointCollecteRepository>();
        services.AddScoped<IPointCollecteRepository, PointCollecteRepository>();
        services.AddScoped<IEquipeRepository, EquipeRepository>();
        services.AddScoped<IOrdreTravailRepository, OrdreTravailRepository>();
        services.AddScoped<IOrdreTravailDetailRepository, OrdreTravailDetailRepository>();
        services.AddScoped<IRattachementRepository, RattachementRepository>();
        services.AddScoped<IShiftRepository, ShiftRepository>();
        services.AddScoped<IBusRepository, BusRepository>();


        
        
        
   
        
       
        return services;
    }
    
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configration)
    {
        var jwtOptions = new JwtOptions();
        configration.Bind(JwtOptions.SectionName, jwtOptions);

        services.AddSingleton(Options.Create(jwtOptions));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.Secret)),

                };
                
                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        throw new UnAuthorizedException("UnAuthorized User.");
                    },
                    OnForbidden = _ =>
                    {
                        throw new ForbiddenException("Forbidden User.");
                    },
                };
            });

        return services;
    }
    
}