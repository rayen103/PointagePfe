# Database Migration Guide

## Overview
This guide explains how to apply the database migration that adds the new entities (Circuit, PointCollecte, Equipe, OrdreTravail, Rattachement, and Employe) to your SQL Server database.

## Prerequisites
- SQL Server instance running and accessible
- Connection string configured in `appsettings.json`
- .NET 8.0 SDK installed
- EF Core tools installed globally: `dotnet tool install --global dotnet-ef`

## Migration Details

### Migration Name
`20260210140931_AddNewEntities`

### Entities Included
1. **Circuit** - Route planning and management
2. **PointCollecte** - Collection points with GPS coordinates
3. **Equipe** - Team management
4. **OrdreTravail** - Work order tracking
5. **Rattachement** - Assignment/attachment system
6. **Employe** - Employee management

### Tables Created
All tables include:
- Primary key (GUID)
- Foreign key to Societe table
- Audit fields (InsererPar, DateInsertion, ModifierPar, DateModification)
- IsActive flag (for most entities)

## How to Apply the Migration

### Method 1: Using dotnet CLI (Recommended)

**From the backend directory:**

```bash
cd backend

dotnet ef database update \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

### Method 2: Windows PowerShell

**From the backend directory:**

```powershell
cd backend

dotnet ef database update `
  --project src\CollectManagement.ms\CollectManagement.Infrastructure\CollectManagement.Infrastructure.csproj `
  --startup-project src\CollectManagement.ms\CollectManagement.WebAPI\CollectManagement.WebAPI.csproj `
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

### Method 3: Apply Specific Migration

**To apply this specific migration:**

```bash
cd backend

dotnet ef database update 20260210140931_AddNewEntities \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

## Troubleshooting

### Build Errors
If you encounter build errors:

```bash
cd backend
dotnet build
```

Check the output for specific errors. The current migration has been verified to build successfully.

### Connection String Issues
Ensure your connection string in `appsettings.json` is correct:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

### Migration Already Applied
If you get an error saying the migration is already applied, check your database:

```bash
dotnet ef migrations list \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

### Rollback Migration
If you need to rollback this migration:

```bash
dotnet ef database update 20260204134052_Initial \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

## Verification

### Check Migration Status

```bash
cd backend

dotnet ef migrations list \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

Expected output should show:
```
20260204134052_Initial (Applied)
20260210140931_AddNewEntities (Applied)
```

### Verify Tables in SQL Server

Connect to your SQL Server database and run:

```sql
-- Check if tables exist
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
AND TABLE_NAME IN ('Circuit', 'PointCollecte', 'Equipe', 'OrdreTravail', 'Rattachement', 'Employe')
ORDER BY TABLE_NAME;
```

Expected result:
```
Circuit
Employe
Equipe
OrdreTravail
PointCollecte
Rattachement
```

### Test API Endpoints

After migration, test that the API endpoints work:

```bash
# Start the API
cd backend/src/CollectManagement.ms/CollectManagement.WebAPI
dotnet run
```

Test endpoints:
- GET `/cm/circuit/list`
- GET `/cm/pointcollecte/list`
- GET `/cm/equipe/list`
- GET `/cm/ordretravail/list`
- GET `/cm/rattachement/list`
- GET `/cm/employe/list` (if endpoint exists)

## Database Schema

### Circuit Table
```sql
CREATE TABLE Circuit (
    CircuitId UNIQUEIDENTIFIER PRIMARY KEY,
    CodeCircuit NVARCHAR(50) NOT NULL,
    LibelleCircuit NVARCHAR(200),
    Description NVARCHAR(500),
    IsActive BIT NOT NULL DEFAULT 1,
    SocieteId UNIQUEIDENTIFIER NOT NULL,
    InsererPar NVARCHAR(MAX),
    DateInsertion DATETIME2,
    ModifierPar NVARCHAR(MAX),
    DateModification DATETIME2,
    FOREIGN KEY (SocieteId) REFERENCES Societe(SocieteId)
);
```

### PointCollecte Table
```sql
CREATE TABLE PointCollecte (
    PointCollecteId UNIQUEIDENTIFIER PRIMARY KEY,
    CodePointCollecte NVARCHAR(50) NOT NULL,
    LibellePointCollecte NVARCHAR(200) NOT NULL,
    Latitude DECIMAL(18,10),
    Longitude DECIMAL(18,10),
    CodeGouvernorat NVARCHAR(50),
    CodeRegion NVARCHAR(50),
    IsActive BIT NOT NULL DEFAULT 1,
    SocieteId UNIQUEIDENTIFIER NOT NULL,
    InsererPar NVARCHAR(MAX),
    DateInsertion DATETIME2,
    ModifierPar NVARCHAR(MAX),
    DateModification DATETIME2,
    FOREIGN KEY (SocieteId) REFERENCES Societe(SocieteId)
);
```

### Equipe Table
```sql
CREATE TABLE Equipe (
    EquipeId UNIQUEIDENTIFIER PRIMARY KEY,
    CodeEquipe NVARCHAR(50) NOT NULL,
    LibelleEquipe NVARCHAR(200),
    CodeClient NVARCHAR(50),
    CodeEntrepot NVARCHAR(50),
    CodeTarif NVARCHAR(50),
    CodeFournisseur NVARCHAR(50),
    Responsable NVARCHAR(100),
    IsInternal BIT NOT NULL DEFAULT 0,
    CodeVehicule NVARCHAR(50),
    IsActive BIT NOT NULL DEFAULT 1,
    SocieteId UNIQUEIDENTIFIER NOT NULL,
    InsererPar NVARCHAR(MAX),
    DateInsertion DATETIME2,
    ModifierPar NVARCHAR(MAX),
    DateModification DATETIME2,
    FOREIGN KEY (SocieteId) REFERENCES Societe(SocieteId)
);
```

### OrdreTravail Table
```sql
CREATE TABLE OrdreTravail (
    OrdreTravailId UNIQUEIDENTIFIER PRIMARY KEY,
    NumeroOrdreTravail NVARCHAR(50) NOT NULL,
    NumeroChantier NVARCHAR(50),
    CodeClient NVARCHAR(50),
    NumeroBonCommande NVARCHAR(50),
    CodeEquipe NVARCHAR(50),
    EtatOT NVARCHAR(50),
    Montant DECIMAL(18,3),
    DateCreation DATE,
    NumeroConvention NVARCHAR(50),
    CodeVehicule NVARCHAR(50),
    Libelle NVARCHAR(200),
    IsActive BIT NOT NULL DEFAULT 1,
    SocieteId UNIQUEIDENTIFIER NOT NULL,
    InsererPar NVARCHAR(MAX),
    DateInsertion DATETIME2,
    ModifierPar NVARCHAR(MAX),
    DateModification DATETIME2,
    FOREIGN KEY (SocieteId) REFERENCES Societe(SocieteId)
);
```

### Rattachement Table
```sql
CREATE TABLE Rattachement (
    RattachementId UNIQUEIDENTIFIER PRIMARY KEY,
    NumeroRattachement NVARCHAR(50) NOT NULL,
    Exercice NVARCHAR(20),
    DateRattachement DATE NOT NULL,
    NumeroChantier NVARCHAR(50),
    CodeClient NVARCHAR(50),
    IsInternal BIT NOT NULL DEFAULT 0,
    Cout DECIMAL(18,3),
    Type NVARCHAR(50),
    Nature NVARCHAR(50),
    Responsable NVARCHAR(100),
    HeureDebut NVARCHAR(10),
    HeureFin NVARCHAR(10),
    Emplacement NVARCHAR(200),
    Reference NVARCHAR(100),
    Status NVARCHAR(50),
    DateCloture DATE,
    Remarque NVARCHAR(500),
    IsActive BIT NOT NULL DEFAULT 1,
    SocieteId UNIQUEIDENTIFIER NOT NULL,
    InsererPar NVARCHAR(MAX),
    DateInsertion DATETIME2,
    ModifierPar NVARCHAR(MAX),
    DateModification DATETIME2,
    FOREIGN KEY (SocieteId) REFERENCES Societe(SocieteId)
);
```

### Employe Table
```sql
CREATE TABLE Employe (
    EmployeId UNIQUEIDENTIFIER PRIMARY KEY,
    Matricule NVARCHAR(50) NOT NULL,
    RFID NVARCHAR(50),
    Nom NVARCHAR(100) NOT NULL,
    Prenom NVARCHAR(100) NOT NULL,
    CodeCircuit NVARCHAR(50),
    CodePointCollecte NVARCHAR(50),
    CodeShift NVARCHAR(50),
    Adresse NVARCHAR(255),
    CodeGouvernorat NVARCHAR(50),
    CodeRegion NVARCHAR(50),
    SocieteId UNIQUEIDENTIFIER NOT NULL,
    InsererPar NVARCHAR(MAX),
    DateInsertion DATETIME2,
    ModifierPar NVARCHAR(MAX),
    DateModification DATETIME2,
    FOREIGN KEY (SocieteId) REFERENCES Societe(SocieteId)
);
```

## Next Steps

After successful migration:

1. **Test CRUD Operations**: Use Swagger UI or Postman to test all endpoints
2. **Verify Data Integrity**: Ensure foreign key constraints work correctly
3. **Frontend Integration**: Update Angular frontend to use new entities
4. **Seed Data**: Add initial/test data if needed
5. **Backup Database**: Create a backup of the database after successful migration

## Support

If you encounter issues:
1. Check the build output for specific errors
2. Verify connection string configuration
3. Ensure SQL Server is running and accessible
4. Check EF Core tools version: `dotnet ef --version`
5. Review migration file for any customization needs

## Additional Commands

### Generate SQL Script (without applying)
```bash
dotnet ef migrations script \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext \
  --output migration.sql
```

### Remove Last Migration (if not applied)
```bash
dotnet ef migrations remove \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

---

**Last Updated**: February 10, 2026  
**Migration Version**: 20260210140931_AddNewEntities  
**Status**: Ready to Apply ✅
