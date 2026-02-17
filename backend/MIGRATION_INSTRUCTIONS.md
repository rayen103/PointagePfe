# Database Migration: Add Latitude and Longitude to Circuit

## Issue
The application was failing with SQL errors:
```
Invalid column name 'Latitude'.
Invalid column name 'Longitude'.
```

This occurred because the `Latitude` and `Longitude` fields were added to the Circuit entity in the code but the database schema was not updated.

## Solution
A database migration has been created to add these columns to the Circuit table.

## Migration Details
- **Migration Name**: `20260217082902_AddLatitudeLongitudeToCircuit`
- **Location**: `src/CollectManagement.ms/CollectManagement.Infrastructure/Migrations/`

### Changes Made
1. Added `Latitude` column to Circuit table (float, nullable)
2. Added `Longitude` column to Circuit table (float, nullable)
3. Updated Rattachement table columns (HeureDebut, HeureFin, Exercice) - these were pending model changes

## How to Apply the Migration

### Method 1: Using Entity Framework CLI (Recommended)

1. **Navigate to the backend directory:**
   ```bash
   cd backend
   ```

2. **Apply the migration:**
   ```bash
   dotnet ef database update --project src/CollectManagement.ms/CollectManagement.Infrastructure --startup-project src/CollectManagement.ms/CollectManagement.WebAPI --context ApplicationDbContext
   ```

3. **Verify the migration:**
   ```bash
   dotnet ef migrations list --project src/CollectManagement.ms/CollectManagement.Infrastructure --startup-project src/CollectManagement.ms/CollectManagement.WebAPI --context ApplicationDbContext
   ```

### Method 2: Using SQL Script Manually

If you prefer to apply the migration manually or need to review the SQL first:

1. **Generate the SQL script:**
   ```bash
   cd backend
   dotnet ef migrations script 20260210142325_AddAllSixEntities 20260217082902_AddLatitudeLongitudeToCircuit --project src/CollectManagement.ms/CollectManagement.Infrastructure --startup-project src/CollectManagement.ms/CollectManagement.WebAPI --context ApplicationDbContext --idempotent -o migration.sql
   ```

2. **Review the generated SQL file** (`migration.sql`)

3. **Execute the SQL script** using SQL Server Management Studio or Azure Data Studio

### Key SQL Changes
The migration adds these columns:
```sql
ALTER TABLE [Circuit] ADD [Latitude] float NULL;
ALTER TABLE [Circuit] ADD [Longitude] float NULL;
```

## Verification

After applying the migration, verify the columns exist:

```sql
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Circuit'
AND COLUMN_NAME IN ('Latitude', 'Longitude');
```

Expected result:
| COLUMN_NAME | DATA_TYPE | IS_NULLABLE |
|-------------|-----------|-------------|
| Latitude    | float     | YES         |
| Longitude   | float     | YES         |

## Connection String

Make sure your connection string in `appsettings.Developement.json` is correct:
```json
"ConnectionStrings": {
  "SqlServerConnection": "Server=YOUR_SERVER;Database=PointageBus;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True"
}
```

## Troubleshooting

### Error: "A network-related or instance-specific error occurred"
- Verify SQL Server is running
- Check firewall settings
- Confirm connection string is correct

### Error: "Login failed for user"
- Verify database credentials
- Ensure user has appropriate permissions

### Error: "Cannot find compilation library location"
- Run `dotnet restore` in the backend directory
- Rebuild the solution: `dotnet build`

## Rolling Back (if needed)

If you need to rollback this migration:

```bash
dotnet ef database update 20260210142325_AddAllSixEntities --project src/CollectManagement.ms/CollectManagement.Infrastructure --startup-project src/CollectManagement.ms/CollectManagement.WebAPI --context ApplicationDbContext
```

Or manually execute:
```sql
ALTER TABLE [Circuit] DROP COLUMN [Latitude];
ALTER TABLE [Circuit] DROP COLUMN [Longitude];
```

## Post-Migration

After applying the migration:
1. Restart the application
2. Test creating a new circuit with location data
3. Test editing an existing circuit to add location
4. Verify the map displays correctly on the circuit details page
