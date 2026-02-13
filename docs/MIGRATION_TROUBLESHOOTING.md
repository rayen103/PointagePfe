# Database Migration Troubleshooting Guide

## Common Migration Errors and Solutions

### 1. "There is already an object named 'X' in the database"

**Error Message:**
```
Microsoft.Data.SqlClient.SqlException (0x80131904): There is already an object named 'Employe' in the database.
```

**Cause:**
- The table already exists in the database
- EF Core migration history doesn't know about it
- Migration is trying to create it again

**Solution:**
Use conditional table creation with IF NOT EXISTS check:

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    // Safe approach - check before creating
    migrationBuilder.Sql(@"
        IF NOT EXISTS (SELECT * FROM sys.objects 
                       WHERE object_id = OBJECT_ID(N'[dbo].[TableName]') 
                       AND type in (N'U'))
        BEGIN
            CREATE TABLE [TableName] (...);
        END
    ");
}
```

**Or manually mark migration as applied:**
```sql
-- Add entry to migrations history without executing
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES ('20260210142325_MigrationName', '10.0.0');
```

### 2. "Build FAILED" When Running Migration

**Error:**
```
Build failed. Use dotnet build to see the errors.
```

**Solution:**
```bash
# Restore NuGet packages first
dotnet restore

# Then try build
dotnet build

# Then run migration
dotnet ef database update ...
```

### 3. "Migration Already Applied to Database"

**Error:**
```
The migration '20260210142325_AddAllSixEntities' has already been applied to the database.
```

**This is Actually Success!**
- If you see this, the migration was already applied
- Database is up to date
- No action needed

**To verify:**
```bash
dotnet ef migrations list
```

Look for asterisk (*) next to applied migrations.

### 4. "Cannot Execute Because Connection String Not Initialized"

**Error:**
```
The ConnectionString property has not been initialized.
```

**Cause:**
- Missing or incorrect connection string in appsettings.json
- Wrong environment configuration

**Solution:**
Check `src/CollectManagement.ms/CollectManagement.WebAPI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 5. Foreign Key Constraint Errors

**Error:**
```
The DELETE statement conflicted with the REFERENCE constraint
```

**Solution:**
Our migrations use `ReferentialAction.Restrict`:
```csharp
onDelete: ReferentialAction.Restrict
```

This prevents accidental cascading deletes and data loss.

### 6. Rolling Back a Migration

**To rollback to previous migration:**
```bash
dotnet ef database update PreviousMigrationName \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

**To rollback to initial state:**
```bash
dotnet ef database update 0 ...
```

### 7. Removing a Migration (Not Yet Applied)

**If migration not applied to database:**
```bash
dotnet ef migrations remove \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

**If migration already applied:**
1. First rollback using `database update PreviousMigration`
2. Then remove using `migrations remove`

### 8. Model Snapshot Out of Sync

**Symptoms:**
- Empty migrations being generated
- Unexpected table drops in migration

**Solution:**
```bash
# Delete all migration files EXCEPT ApplicationDbContextModelSnapshot.cs
# Recreate migration from scratch
dotnet ef migrations add RecreateFromScratch ...
```

### 9. Multiple Migrations Show Up But Should Be One

**Cause:**
- Multiple developers working on migrations
- Merge conflicts in migrations

**Solution:**
```bash
# Coordinate with team
# Decide on one migration to keep
# Remove others using migrations remove
# Merge changes into one comprehensive migration
```

### 10. "Asset File Not Found" Error

**Error:**
```
Assets file 'obj/project.assets.json' not found
```

**Solution:**
```bash
dotnet restore
```

## Best Practices

### ✅ DO:
1. **Always backup database before applying migrations**
   ```sql
   BACKUP DATABASE YourDatabase TO DISK = 'C:\Backup\before_migration.bak'
   ```

2. **Test migrations on development database first**
   
3. **Use meaningful migration names**
   ```bash
   dotnet ef migrations add AddUserEmailField
   # NOT: dotnet ef migrations add Update1
   ```

4. **Review generated migration before applying**
   ```bash
   # Check the .cs file in Migrations folder
   ```

5. **Keep migrations small and focused**
   - One feature per migration when possible

6. **Use transactions for data migrations**
   ```csharp
   migrationBuilder.Sql("BEGIN TRANSACTION; ... COMMIT;");
   ```

### ❌ DON'T:
1. **Never edit applied migrations**
   - Create a new migration instead

2. **Never delete migrations that are in production**
   - Use rollback if needed

3. **Don't run migrations directly in production**
   - Use deployment scripts
   - Review and test first

4. **Don't ignore migration errors**
   - Always investigate and fix properly

## Verification Checklist

After applying migration:

- [ ] Check migration is in migrations history:
  ```sql
  SELECT * FROM __EFMigrationsHistory ORDER BY MigrationId;
  ```

- [ ] Verify all expected tables exist:
  ```sql
  SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';
  ```

- [ ] Check foreign keys are created:
  ```sql
  SELECT * FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS;
  ```

- [ ] Verify indexes exist:
  ```sql
  SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('TableName');
  ```

- [ ] Test API endpoints work:
  - Open Swagger UI
  - Try GET, POST, PUT, DELETE operations

## Getting Help

If you encounter an error not listed here:

1. **Check the error message carefully**
   - Often contains the exact cause

2. **Review migration file**
   - Look at the Up() and Down() methods

3. **Check database state**
   ```sql
   -- See what migrations are applied
   SELECT * FROM __EFMigrationsHistory;
   
   -- See what tables exist
   SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES;
   ```

4. **Build logs**
   ```bash
   dotnet build > build.log 2>&1
   # Check build.log for details
   ```

5. **Enable verbose logging**
   ```bash
   dotnet ef database update --verbose
   ```

## Specific Fix: AddAllSixEntities Migration

For the current migration in this project:

**Migration**: `20260210142325_AddAllSixEntities`

**Issue Resolved**: Employe table already existed in database

**Safe to apply**: Yes - uses IF NOT EXISTS check

**Command**:
```bash
cd backend
dotnet ef database update \
  --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj \
  --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
  --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

**What it does**:
- Checks if Employe exists, creates only if needed
- Creates Circuit, PointCollecte, Equipe, OrdreTravail, Rattachement tables
- Adds all necessary foreign keys and indexes

**No data loss**: Migration only creates new tables, doesn't modify existing ones

---

*Last Updated: February 10, 2026*
*Project: PointagePfe - CollectManagement.ms*
