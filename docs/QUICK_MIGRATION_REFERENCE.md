# Quick Migration Reference

## TL;DR - Apply Migration Now

### Single Command (Linux/Mac)
```bash
cd backend && dotnet ef database update --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

### Single Command (Windows)
```powershell
cd backend; dotnet ef database update --project src\CollectManagement.ms\CollectManagement.Infrastructure\CollectManagement.Infrastructure.csproj --startup-project src\CollectManagement.ms\CollectManagement.WebAPI\CollectManagement.WebAPI.csproj --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

## What This Does

✅ Creates 6 new tables:
1. Circuit (route planning)
2. PointCollecte (GPS collection points)
3. Equipe (teams)
4. OrdreTravail (work orders)
5. Rattachement (assignments)
6. Employe (employees)

✅ All tables linked to Societe via foreign key  
✅ All have audit fields (InsererPar, DateInsertion, etc.)  
✅ All have IsActive flag (except Employe)  

## Verification

### Check if migration applied successfully:
```bash
cd backend && dotnet ef migrations list --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

Look for:
```
✓ 20260204134052_Initial (Applied)
✓ 20260210140931_AddNewEntities (Applied)
```

### Check database:
```sql
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME IN ('Circuit', 'PointCollecte', 'Equipe', 'OrdreTravail', 'Rattachement', 'Employe')
```

Should return all 6 table names.

## API Endpoints Now Available

After migration, these endpoints work:

| Endpoint | Description |
|----------|-------------|
| `/cm/circuit/list` | List circuits with pagination |
| `/cm/circuit/add` | Create new circuit |
| `/cm/circuit/update` | Update circuit |
| `/cm/circuit/{id}/delete` | Delete circuit |
| `/cm/circuit/{id}/one` | Get single circuit |
| `/cm/pointcollecte/*` | Same CRUD for PointCollecte |
| `/cm/equipe/*` | Same CRUD for Equipe |
| `/cm/ordretravail/*` | Same CRUD for OrdreTravail |
| `/cm/rattachement/*` | Same CRUD for Rattachement |

## Troubleshooting

### Build failed?
```bash
cd backend && dotnet build
```
Check output. Current migration builds successfully (0 errors).

### Connection issue?
Check `appsettings.json` connection string.

### Already applied?
That's OK! EF Core will skip it automatically.

### Need to rollback?
```bash
cd backend && dotnet ef database update 20260204134052_Initial --project src/CollectManagement.ms/CollectManagement.Infrastructure/CollectManagement.Infrastructure.csproj --startup-project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj --context CollectManagement.Infrastructure.Persistence.Context.ApplicationDbContext
```

## Files Changed

```
backend/src/CollectManagement.ms/CollectManagement.Infrastructure/Migrations/
├── 20260210140931_AddNewEntities.cs ← NEW MIGRATION
├── 20260210140931_AddNewEntities.Designer.cs ← NEW
└── ApplicationDbContextModelSnapshot.cs ← UPDATED
```

## Next Steps

1. ✅ Apply migration (command above)
2. ✅ Verify tables created (SQL query above)
3. ✅ Test API endpoints (Swagger: `/swagger`)
4. ✅ Continue with frontend development

---

**Migration**: `20260210140931_AddNewEntities`  
**Status**: ✅ Ready to apply  
**Build**: ✅ 0 errors  
**Entities**: 6 new tables  
