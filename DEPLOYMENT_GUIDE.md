# Deployment Guide - After Merging Changes to Main

## Issue: "No Changes Visible After Merge"

If you've merged changes to the `main` branch but don't see any updates when running the application, this guide will help you resolve the issue.

---

## Root Cause

After merging code changes (especially when new dependencies are added), you need to:
1. **Install/update dependencies** - The `node_modules` folder is not committed to Git
2. **Rebuild the application** - Ensure all new code is compiled
3. **Restart the development server** - Load the new changes

---

## Quick Fix (Frontend)

If you merged changes and the **Angular frontend** isn't showing updates:

```bash
# Navigate to frontend directory
cd frontend

# Install/update all dependencies (including new ones from package.json)
npm install

# Start the development server
npm start
```

The app will be available at `http://localhost:4200`

---

## Quick Fix (Backend)

If you merged changes and the **.NET backend** isn't showing updates:

```bash
# Navigate to backend directory
cd backend

# Restore NuGet packages
dotnet restore

# Build the solution
dotnet build

# Run the application
dotnet run --project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj
```

---

## What Was Recently Added (Map Feature)

The recent merge added **Leaflet map integration** for visualizing circuit locations:

### New Components:
- **MapPickerComponent** - Interactive map for selecting circuit locations (drag & drop marker)
- **MapViewerComponent** - Read-only map displaying multiple locations with color-coded markers

### New Dependencies:
```json
"leaflet": "^1.9.4",
"@asymmetrik/ngx-leaflet": "^18.0.1",
"@types/leaflet": "^1.9.12",
"leaflet-color-markers": "^0.1.0"
```

### Where to See It:
1. Navigate to the **Circuit** module in the admin panel
2. Create or edit a circuit
3. You'll see an interactive map where you can place/drag markers to set the location

---

## Complete Deployment Steps

### Frontend Deployment

1. **Clean previous builds** (optional, but recommended):
   ```bash
   cd frontend
   rm -rf node_modules package-lock.json dist
   ```

2. **Install dependencies**:
   ```bash
   npm install
   ```

3. **Development mode**:
   ```bash
   npm start
   ```

4. **Production build**:
   ```bash
   npm run build
   ```
   Output will be in `frontend/dist/`

### Backend Deployment

1. **Clean previous builds** (optional):
   ```bash
   cd backend
   dotnet clean
   ```

2. **Restore packages**:
   ```bash
   dotnet restore
   ```

3. **Build**:
   ```bash
   dotnet build
   ```

4. **Run in development**:
   ```bash
   dotnet run --project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj
   ```

5. **Run in production**:
   ```bash
   dotnet publish -c Release
   # Then deploy the published output
   ```

---

## Troubleshooting

### Issue: "Map not displaying"

**Symptoms**: Circuit form loads, but map area is blank or broken

**Solutions**:
1. Check browser console for JavaScript errors (F12)
2. Verify Leaflet CSS is loaded:
   ```bash
   # Check if file exists
   ls frontend/node_modules/leaflet/dist/leaflet.css
   ```
3. Clear browser cache (Ctrl+F5)
4. Restart the development server

### Issue: "npm install fails"

**Solutions**:
1. Check Node.js version (should be compatible with Angular 18):
   ```bash
   node --version  # Should be >= 18.13.0
   npm --version
   ```
2. Clear npm cache:
   ```bash
   npm cache clean --force
   npm install
   ```

### Issue: "Backend won't start"

**Solutions**:
1. Check .NET SDK version:
   ```bash
   dotnet --version  # Should be >= 6.0
   ```
2. Check for port conflicts (default: 5000/5001)
3. Verify database connection string in `appsettings.json`

### Issue: "Changes not visible after npm install"

**Solutions**:
1. Hard refresh browser: `Ctrl+Shift+R` (Windows/Linux) or `Cmd+Shift+R` (Mac)
2. Stop and restart the dev server:
   ```bash
   # Press Ctrl+C to stop
   npm start  # Start again
   ```
3. Clear Angular cache:
   ```bash
   rm -rf frontend/.angular/cache
   npm start
   ```

---

## Environment Setup (First Time)

### Prerequisites

**Frontend**:
- Node.js >= 18.13.0
- npm >= 9.0.0

**Backend**:
- .NET SDK >= 6.0
- SQL Server (or connection to existing database)

### Initial Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/rayen103/PointagePfe.git
   cd PointagePfe
   ```

2. **Setup frontend**:
   ```bash
   cd frontend
   npm install
   ```

3. **Setup backend**:
   ```bash
   cd backend
   dotnet restore
   ```

4. **Configure environment**:
   - Update `backend/src/CollectManagement.ms/CollectManagement.WebAPI/appsettings.json`
   - Set database connection string
   - Configure API URLs if needed

5. **Run migrations** (if applicable):
   ```bash
   cd backend
   dotnet ef database update
   ```

---

## Best Practices After Merging

1. **Always run `npm install`** after pulling/merging changes
2. **Check `package.json`** for new dependencies
3. **Restart development servers** to load new code
4. **Clear browser cache** if styles/scripts don't update
5. **Check for migration files** if database schema changed

---

## Production Deployment Checklist

- [ ] Pull latest changes from `main` branch
- [ ] Run `npm install` in frontend directory
- [ ] Run `dotnet restore` in backend directory
- [ ] Update environment variables/configuration files
- [ ] Run database migrations (if any)
- [ ] Build frontend: `npm run build`
- [ ] Build backend: `dotnet publish -c Release`
- [ ] Deploy built artifacts to production server
- [ ] Restart services
- [ ] Verify all features work in production
- [ ] Check browser console for errors
- [ ] Test map functionality specifically

---

## Getting Help

If issues persist:
1. Check browser console (F12) for JavaScript errors
2. Check backend logs for API errors
3. Verify all services are running
4. Ensure database is accessible
5. Check network tab in browser for failed requests

---

## Summary

**The most common issue**: Not running `npm install` after merging changes that add new dependencies.

**Quick solution**: 
```bash
cd frontend && npm install && npm start
```

This will install the new Leaflet map dependencies and start the application with all the latest features!
