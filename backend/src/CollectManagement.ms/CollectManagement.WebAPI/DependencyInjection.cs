
using Carter;
using CollectManagement.WebAPI.Common.Converters;
using Microsoft.OpenApi.Models;

namespace CollectManagement.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
        services.AddCarter();

        // Support "HH:mm" / "HH:mm:ss" TimeSpan values sent by the Angular frontend
        services.ConfigureHttpJsonOptions(o =>
        {
            o.SerializerOptions.Converters.Add(new TimeSpanJsonConverter());
            o.SerializerOptions.Converters.Add(new NullableTimeSpanJsonConverter());
        });

        return services;
    }
    
    public static IServiceCollection AddSwagger(
        this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "JWT",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    Array.Empty<string>()
                }
            });

        });
        
        return services;
    }
}