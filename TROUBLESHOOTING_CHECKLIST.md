# Troubleshooting Checklist: "No Changes After Merge"

Use this checklist when you merge changes but don't see them when running the application.

---

## Quick Diagnosis

Run these commands to check the status:

```bash
# Check which branch you're on
git branch

# Check if you have the latest code
git status
git log --oneline -5

# Check if dependencies are installed
ls frontend/node_modules/ | wc -l    # Should show many folders (1000+)
ls backend/src/                       # Should show project folders
```

---

## Step-by-Step Resolution

### ✅ Step 1: Verify You're on the Correct Branch

```bash
git branch
```

**Expected**: Should show `* main` (or the branch you merged to)

**If wrong**:
```bash
git checkout main
git pull origin main
```

---

### ✅ Step 2: Ensure You Have Latest Code

```bash
git pull origin main
```

**Expected**: "Already up to date" or successful pull message

**If conflicts**: Resolve merge conflicts first

---

### ✅ Step 3: Install Frontend Dependencies

```bash
cd frontend
npm install
```

**Expected**: 
- "added XXX packages" message
- No fatal errors
- `node_modules/` folder created/updated

**If errors**:
- Check Node.js version: `node --version` (need >= 18.13.0)
- Clear cache: `npm cache clean --force`
- Delete and retry: `rm -rf node_modules package-lock.json && npm install`

---

### ✅ Step 4: Verify Map Dependencies Installed

```bash
cd frontend
ls node_modules/leaflet/
```

**Expected**: Should show folders like `dist/`, `src/`, etc.

**If missing**: Run `npm install` again

---

### ✅ Step 5: Restore Backend Dependencies

```bash
cd backend
dotnet restore
```

**Expected**: 
- "Restored X projects" messages
- No errors

**If errors**:
- Check .NET version: `dotnet --version` (need >= 6.0)
- Check internet connection (NuGet packages need to download)

---

### ✅ Step 6: Start Frontend Development Server

```bash
cd frontend
npm start
```

**Expected**:
- Compilation messages
- "Compiled successfully"
- "Local: http://localhost:4200"

**If errors**:
- Check for TypeScript errors in output
- Verify all imports are correct
- Check angular.json for configuration issues

---

### ✅ Step 7: Start Backend Server

```bash
cd backend
dotnet run --project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj
```

**Expected**:
- "Now listening on: http://localhost:5000"
- No compilation errors

**If errors**:
- Check database connection string in appsettings.json
- Verify all NuGet packages restored correctly
- Check for missing configuration files

---

### ✅ Step 8: Test in Browser

1. Open: `http://localhost:4200`
2. Open browser console (F12)
3. Check for JavaScript errors
4. Navigate to the feature you're testing

**For Map Feature**:
1. Go to Admin → Circuits
2. Create or edit a circuit
3. Scroll down to see the map
4. Click on map to place a marker

**Expected**:
- Map tiles load (shows actual map)
- Marker appears on click/drag
- No console errors

**If map doesn't show**:
- Hard refresh: `Ctrl+Shift+F5`
- Check console for errors
- Verify Leaflet CSS loaded (check Network tab)

---

## Common Error Messages & Solutions

### Error: "Module not found: Error: Can't resolve 'leaflet'"

**Cause**: Dependencies not installed

**Solution**:
```bash
cd frontend
rm -rf node_modules package-lock.json
npm install
```

---

### Error: "Cannot find module '@angular/xxx'"

**Cause**: Angular packages missing or version mismatch

**Solution**:
```bash
cd frontend
npm install
# Or force reinstall:
rm -rf node_modules package-lock.json .angular
npm install
```

---

### Error: "The project file could not be loaded"

**Cause**: .NET project file corrupted or wrong path

**Solution**:
```bash
cd backend
dotnet restore --force
dotnet build
```

---

### Error: "Port 4200 already in use"

**Cause**: Another instance of the app is running

**Solution**:
```bash
# Kill the process using the port
lsof -i :4200  # Find the PID
kill -9 <PID>  # Kill the process
# Or use a different port:
ng serve --port 4201
```

---

### Error: "A connection to the database could not be established"

**Cause**: Database connection string wrong or database not running

**Solution**:
1. Check connection string in `backend/appsettings.json`
2. Verify SQL Server is running
3. Test connection with SQL client
4. Run migrations: `dotnet ef database update`

---

## Verification Checklist

After following all steps, verify:

- [ ] `git branch` shows correct branch
- [ ] `git status` shows "working tree clean"
- [ ] `frontend/node_modules/` folder exists with 1000+ packages
- [ ] `frontend/node_modules/leaflet/` folder exists
- [ ] `backend/` restored without errors
- [ ] Frontend starts without compilation errors
- [ ] Backend starts without errors
- [ ] Browser can access `http://localhost:4200`
- [ ] No console errors in browser (F12)
- [ ] Map feature visible in Circuit module
- [ ] Map tiles load (shows actual map, not blank)
- [ ] Can interact with map (click, drag)

---

## Still Having Issues?

### Check These Files

1. **frontend/package.json**
   - Verify leaflet packages are listed in dependencies:
     ```json
     "leaflet": "^1.9.4",
     "@asymmetrik/ngx-leaflet": "^18.0.1"
     ```

2. **frontend/angular.json**
   - Verify Leaflet CSS is in styles array:
     ```json
     "styles": [
       "node_modules/leaflet/dist/leaflet.css"
     ]
     ```

3. **frontend/src/app/shared/components/map-picker/**
   - Verify component files exist:
     - `map-picker.component.ts`
     - `map-picker.component.html`
     - `map-picker.component.scss`

### Check Environment

```bash
# Node.js version
node --version  # Should be >= 18.13.0

# npm version
npm --version   # Should be >= 9.0.0

# .NET version
dotnet --version  # Should be >= 6.0

# Git status
git status
git log --oneline -5
```

### Get Detailed Logs

```bash
# Frontend logs
cd frontend
npm start > frontend.log 2>&1 &
tail -f frontend.log

# Backend logs
cd backend
dotnet run > backend.log 2>&1 &
tail -f backend.log
```

---

## Nuclear Option (Complete Reset)

If nothing else works, try a complete fresh start:

```bash
# Backup your local changes if needed
git stash

# Get fresh copy of main
git checkout main
git fetch origin
git reset --hard origin/main

# Clean everything
cd frontend
rm -rf node_modules package-lock.json .angular dist
cd ..

# Fresh install
cd frontend && npm install
cd ../backend && dotnet clean && dotnet restore

# Start fresh
cd frontend && npm start
```

---

## Success Criteria

You know it's working when:

✅ Frontend compiles without errors
✅ Backend starts without errors  
✅ Browser loads the app at `http://localhost:4200`
✅ No red errors in browser console (F12)
✅ Map appears in Circuit module
✅ Map shows actual map tiles (streets, cities)
✅ Can click/drag markers on the map
✅ Form updates when marker moves

---

## Need More Help?

1. **Check Documentation**:
   - [`DEPLOYMENT_GUIDE.md`](./DEPLOYMENT_GUIDE.md) - Full deployment guide
   - [`MAP_FEATURE_SETUP.md`](./MAP_FEATURE_SETUP.md) - Map feature details
   - [`README.md`](./README.md) - Project overview

2. **Debug Information to Collect**:
   - Node.js version: `node --version`
   - npm version: `npm --version`
   - .NET version: `dotnet --version`
   - Git branch: `git branch`
   - Git log: `git log --oneline -5`
   - Browser console errors (F12 → Console tab)
   - Frontend errors from `npm start` output
   - Backend errors from `dotnet run` output

3. **Create GitHub Issue** with:
   - What you expected to happen
   - What actually happened
   - Error messages (full text)
   - Steps you already tried
   - Debug information from above

---

**Remember**: 99% of "no changes after merge" issues are solved by running `npm install` in the frontend directory! 🚀
