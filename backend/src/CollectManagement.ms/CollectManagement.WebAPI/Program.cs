using Carter;
using CollectManagement.Application;
using CollectManagement.Infrastructure;
using CollectManagement.Infrastructure.Persistence.Context;
using CollectManagement.WebAPI.Authorization;
using Microsoft.AspNetCore.Authorization;
using CollectManagement.WebAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Rin logger
builder.Logging.AddRinLogger();
builder.Services.AddRin();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddPresentation();

builder.Services.AddCors();
builder.Services.AddScoped<IAuthorizationHandler, NavigationPermissionHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(NavigationPermissionRequirement.PolicyName, policyBuilder =>
        policyBuilder.RequireAuthenticatedUser().AddRequirements(new NavigationPermissionRequirement()));
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Apply any pending EF Core migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
}

//Handle exceptions priority it's important
app.UseExceptionHandler((_) => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseRin();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseRinDiagnosticsHandler();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
app.UseCors(policyBuilder  =>
{
    policyBuilder
        .WithOrigins(allowedOrigins)
        .WithMethods("GET","POST","PUT","PATCH","DELETE")
        .WithHeaders("Authorization", "Content-Type");
});

app.UseAuthentication()
    .UseAuthorization();

// Serve static files from the "public" folder
// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Public")),
//     RequestPath = "/public",
// });
//
// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "uploads", "images", "typeMesure")),
//     RequestPath = "/uploads/images/typeMesure", // On s'assure que cette URL soit utilisée pour accéder aux images
// });

app.MapCarter();

app.Run();