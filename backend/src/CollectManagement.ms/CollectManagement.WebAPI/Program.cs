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

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Developement.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

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

    // 1. Direct resilient seed for critical demo records
    try
    {
        dbContext.Database.ExecuteSqlRaw(@"
-- 1. Ensure intercolor Societe exists
IF NOT EXISTS (SELECT 1 FROM dbo.Societe WHERE SocieteId = '019970F9-BA22-F7A8-1E5C-E9D1206BF8B5')
BEGIN
    INSERT INTO dbo.Societe (SocieteId, Nom, MatriculeFiscal, Capital, DateOverture)
    VALUES ('019970F9-BA22-F7A8-1E5C-E9D1206BF8B5', 'intercolor', '0020515K/A/M/000', 1134700000, '2025-09-22');
END;

-- 2. Ensure CST Societe exists
IF NOT EXISTS (SELECT 1 FROM dbo.Societe WHERE SocieteId = '018B1055-D0B7-DE38-752F-1B18F580C2E0')
BEGIN
    INSERT INTO dbo.Societe (SocieteId, Nom, MatriculeFiscal, Capital, DateOverture)
    VALUES ('018B1055-D0B7-DE38-752F-1B18F580C2E0', 'CST', 'MF-CST-001', 0, '2024-01-01');
END;

-- 3. Ensure rayen103 exists and is active
IF NOT EXISTS (SELECT 1 FROM dbo.Utilisateur WHERE NomUtilisateur = 'rayen103')
BEGIN
    INSERT INTO dbo.Utilisateur (UtilisateurId, NomUtilisateur, Nom, Prenom, Email, Password, IsActive, SocieteId)
    VALUES ('019ECC22-A4E6-267F-50A1-3A04B83ADEDC', 'rayen103', 'farhani', 'rayen', 'rayenfarhani9@gmail.com', 'F0076EA5CFBF1D777D3ECF577CE998EDA1AD96FEDB22C42F0D449A7F7AAF023F6ABD0112972C3EAE86ED0E9A82C01FCFE88249523CE56E6212AE12AC1A7D871F', 1, '019970F9-BA22-F7A8-1E5C-E9D1206BF8B5');
END;

-- 4. Ensure root exists and is active
IF NOT EXISTS (SELECT 1 FROM dbo.Utilisateur WHERE NomUtilisateur = 'root')
BEGIN
    INSERT INTO dbo.Utilisateur (UtilisateurId, NomUtilisateur, Nom, Prenom, Email, Password, IsActive, SocieteId)
    VALUES ('019C2903-0D54-105D-FA74-08F82A436369', 'root', 'root', 'root', 'root@root.com', '919D2AF144DD35A05ADF97786560176B8FCCE3A82B86ABC571AE94B20D2537D7C3677E4AE831B06CC02E1CD0189D1A63B106B2E4EB24581E01E88E31BCEE4FC8', 1, '019970F9-BA22-F7A8-1E5C-E9D1206BF8B5');
END;

-- 5. Activate all users
UPDATE dbo.Utilisateur SET IsActive = 1;

-- 6. Ensure rayenfarhani9@gmail.com has username rayen103 and CST SocieteId
UPDATE dbo.Utilisateur 
SET NomUtilisateur = 'rayen103', 
    IsActive = 1,
    SocieteId = '01HC85BM5QVRW7ABRV33TR1GQ0',
    Password = 'F0076EA5CFBF1D777D3ECF577CE998EDA1AD96FEDB22C42F0D449A7F7AAF023F6ABD0112972C3EAE86ED0E9A82C01FCFE88249523CE56E6212AE12AC1A7D871F'
WHERE Email = 'rayenfarhani9@gmail.com' OR NomUtilisateur = 'rayen103';

UPDATE dbo.Utilisateur
SET SocieteId = '01HC85BM5QVRW7ABRV33TR1GQ0', IsActive = 1
WHERE NomUtilisateur IN ('admin', 'root');

-- 7. Ensure all buses and employees are active
UPDATE dbo.Bus SET IsActive = 1;
UPDATE dbo.Employe SET IsActive = 1;
");
        Log.Information("Core demo accounts & societes seeded.");
    }
    catch (Exception coreEx)
    {
        Log.Warning("Core demo seed notice: {Message}", coreEx.Message);
    }

    // 2. Comprehensive idempotent seed from SeedData.sql
    try
    {
        var seedPath = Path.Combine(AppContext.BaseDirectory, "SeedData.sql");
        string? seedSql = null;
        if (File.Exists(seedPath))
        {
            seedSql = File.ReadAllText(seedPath);
        }
        else
        {
            var assembly = typeof(Program).Assembly;
            var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith("SeedData.sql", StringComparison.OrdinalIgnoreCase));
            if (resourceName != null)
            {
                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream != null)
                {
                    using var reader = new StreamReader(stream);
                    seedSql = reader.ReadToEnd();
                }
            }
        }

        if (!string.IsNullOrWhiteSpace(seedSql))
        {
            dbContext.Database.SetCommandTimeout(180);
            var blocks = seedSql.Split(new[] { "END;" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var block in blocks)
            {
                var trimmed = block.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith("--")) continue;
                try
                {
                    dbContext.Database.ExecuteSqlRaw(trimmed + " END;");
                }
                catch
                {
                    // Ignore non-critical individual constraint collisions
                }
            }
            Log.Information("SeedData.sql blocks executed.");
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Failed to apply SeedData.sql on startup");
    }
}

//Handle exceptions priority it's important
app.UseExceptionHandler((_) => { });

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseRin();
    app.UseRinDiagnosticsHandler();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseCors(policyBuilder =>
{
    policyBuilder
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
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

app.MapGet("/", () => Results.Ok(new { status = "Healthy", service = "CollectManagement.WebAPI", version = "1.0.0" }));

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