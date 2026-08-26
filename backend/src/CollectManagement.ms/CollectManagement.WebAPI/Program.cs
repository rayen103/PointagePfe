using Carter;
using CollectManagement.Application;
using CollectManagement.Application.Interfaces.Services;
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
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    
    // Add missing columns and fix defaults
        dbContext.Database.ExecuteSqlRaw(@"
-- Fix Region table
IF COL_LENGTH('dbo.Region', 'CodeGouvernorat') IS NULL
BEGIN
    ALTER TABLE [dbo].[Region] ADD [CodeGouvernorat] nvarchar(50) NULL;
END;
IF COL_LENGTH('dbo.Region', 'IsActive') IS NULL
BEGIN
    ALTER TABLE [dbo].[Region] ADD [IsActive] bit NOT NULL CONSTRAINT [DF_Region_IsActive] DEFAULT CAST(1 AS bit);
END
ELSE
BEGIN
    IF OBJECT_ID('DF_Region_IsActive', 'D') IS NULL
    BEGIN
        ALTER TABLE [dbo].[Region] ADD CONSTRAINT [DF_Region_IsActive] DEFAULT CAST(1 AS bit) FOR [IsActive];
    END;
END;

-- Fix Modem table
IF COL_LENGTH('dbo.Modem', 'ModelModem') IS NULL
BEGIN
    ALTER TABLE [dbo].[Modem] ADD [ModelModem] nvarchar(100) NULL;
END;
IF COL_LENGTH('dbo.Modem', 'NumeroSim') IS NULL
BEGIN
    ALTER TABLE [dbo].[Modem] ADD [NumeroSim] nvarchar(50) NULL;
END;
IF COL_LENGTH('dbo.Modem', 'IsActive') IS NULL
BEGIN
    ALTER TABLE [dbo].[Modem] ADD [IsActive] bit NOT NULL CONSTRAINT [DF_Modem_IsActive] DEFAULT CAST(1 AS bit);
END
ELSE
BEGIN
    IF OBJECT_ID('DF_Modem_IsActive', 'D') IS NULL
    BEGIN
        ALTER TABLE [dbo].[Modem] ADD CONSTRAINT [DF_Modem_IsActive] DEFAULT CAST(1 AS bit) FOR [IsActive];
    END;
END;

-- Fix Chauffeur table
IF COL_LENGTH('dbo.Chauffeur', 'IsActive') IS NULL
BEGIN
    ALTER TABLE [dbo].[Chauffeur] ADD [IsActive] bit NOT NULL CONSTRAINT [DF_Chauffeur_IsActive] DEFAULT CAST(1 AS bit);
END
ELSE
BEGIN
    IF OBJECT_ID('DF_Chauffeur_IsActive', 'D') IS NULL
    BEGIN
        ALTER TABLE [dbo].[Chauffeur] ADD CONSTRAINT [DF_Chauffeur_IsActive] DEFAULT CAST(1 AS bit) FOR [IsActive];
    END;
END;

-- Fix Gouvernorat table
IF COL_LENGTH('dbo.Gouvernorat', 'IsActive') IS NULL
BEGIN
    ALTER TABLE [dbo].[Gouvernorat] ADD [IsActive] bit NOT NULL CONSTRAINT [DF_Gouvernorat_IsActive] DEFAULT CAST(1 AS bit);
END
ELSE
BEGIN
    IF OBJECT_ID('DF_Gouvernorat_IsActive', 'D') IS NULL
    BEGIN
        ALTER TABLE [dbo].[Gouvernorat] ADD CONSTRAINT [DF_Gouvernorat_IsActive] DEFAULT CAST(1 AS bit) FOR [IsActive];
    END;
END;
");
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

// Auto-start ML ETA Prediction Service on backend startup
if (app.Environment.IsDevelopment())
{
    try
    {
        var solutionRoot = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, "..", "..", ".."));
        var mlServicePath = Path.Combine(solutionRoot, "ml-service");

        if (Directory.Exists(mlServicePath))
        {
            var venvPythonWin = Path.Combine(mlServicePath, ".venv", "Scripts", "python.exe");
            var venvPythonUnix = Path.Combine(mlServicePath, ".venv", "bin", "python");

            string pythonExec;
            if (OperatingSystem.IsWindows() && File.Exists(venvPythonWin))
            {
                pythonExec = venvPythonWin;
            }
            else if (!OperatingSystem.IsWindows() && File.Exists(venvPythonUnix))
            {
                pythonExec = venvPythonUnix;
            }
            else
            {
                pythonExec = OperatingSystem.IsWindows() ? "python" : "python3";
            }

            Console.WriteLine($"🚀 Starting ML ETA Prediction Service automatically using '{pythonExec}'...");

            var processStartInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = pythonExec,
                Arguments = "-m uvicorn main:app --host 0.0.0.0 --port 8000 --reload",
                WorkingDirectory = mlServicePath,
                UseShellExecute = true,
                CreateNoWindow = false,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
            };

            var mlProcess = System.Diagnostics.Process.Start(processStartInfo);

            if (mlProcess != null)
            {
                Console.WriteLine($"✅ ML Service started (PID: {mlProcess.Id}) at http://localhost:8000");

                AppDomain.CurrentDomain.ProcessExit += (s, e) =>
                {
                    if (!mlProcess.HasExited)
                    {
                        Console.WriteLine("🛑 Stopping ML ETA Prediction Service...");
                        mlProcess.Kill();
                        mlProcess.WaitForExit();
                    }
                };
            }
        }
        else
        {
            Console.WriteLine("⚠️ ML service directory not found at: " + mlServicePath);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Failed to auto-start ML service: {ex.Message}");
    }

    // Auto-trigger ML Initialization and Warmup on startup
    Task.Run(async () =>
    {
        await Task.Delay(3000);
        try
        {
            using var scope = app.Services.CreateScope();
            var predictionService = scope.ServiceProvider.GetService<IExternalPredictionService>();
            if (predictionService != null)
            {
                var meta = await predictionService.GetModelMetadataAsync();
                Console.WriteLine($"🤖 ML Service Initialized: Version={meta.ModelVersion}, DurationSamples={meta.DurationSampleCount}, AbsenceSamples={meta.AbsenceSampleCount}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ ML Warmup trigger warning: {ex.Message}");
        }
    });
}

app.Run();