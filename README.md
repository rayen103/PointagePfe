# PointagePfe - Time Tracking & Circuit Management System

This repository contains a full-stack application for managing time tracking (Pointage) and circuit management with integrated map functionality.

---

## Project Structure

```
PointagePfe/
├── frontend/          # Angular 18 web application
├── backend/           # .NET Core Web API
├── Intervention/      # Legacy intervention management system
└── docs/             # Documentation
```

---

## Quick Start

### ⚠️ IMPORTANT: After Cloning or Pulling Changes

**Always run these commands after cloning or pulling updates:**

```bash
# Install frontend dependencies
cd frontend
npm install

# Install backend dependencies
cd ../backend
dotnet restore
```

**Without running `npm install`, new features (like the map integration) won't work!**

---

## Frontend (Angular)

### Prerequisites
- Node.js >= 18.13.0
- npm >= 9.0.0

### Setup & Run
```bash
cd frontend
npm install          # Install dependencies
npm start           # Start dev server at http://localhost:4200
```

### Build for Production
```bash
npm run build       # Output in frontend/dist/
```

### Technology Stack
- **Angular 18** - Frontend framework
- **Angular Material** - UI components
- **Tailwind CSS** - Utility-first CSS
- **Leaflet** - Interactive maps
- **DevExtreme** - Advanced data grids
- **Transloco** - Internationalization

---

## Backend (.NET)

### Prerequisites
- .NET SDK >= 6.0
- SQL Server (or compatible database)

### Setup & Run
```bash
cd backend
dotnet restore                 # Restore NuGet packages
dotnet build                   # Build solution

# Run the API
dotnet run --project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj
```

### Configuration
1. Update database connection string in `appsettings.json`
2. Run migrations: `dotnet ef database update` (if applicable)

### Technology Stack
- **.NET Core** - Backend framework
- **Entity Framework Core** - ORM
- **Clean Architecture** - Project structure

---

## Recent Updates

### ✨ Map Integration (Latest Merge)

The application now includes **interactive map functionality** for visualizing circuit locations:

- **MapPickerComponent**: Drag-and-drop interface for selecting locations
- **MapViewerComponent**: Display multiple circuits on a single map
- **Color-coded markers**: Green for active, red for inactive circuits
- **OpenStreetMap integration**: No API key required!

**To see this feature after merging:**
```bash
cd frontend
npm install  # Installs new Leaflet dependencies
npm start
```

Then navigate to **Admin → Circuits** to see the map!

📖 **Detailed documentation**: See [`MAP_FEATURE_SETUP.md`](./MAP_FEATURE_SETUP.md)

---

## Common Issues

### ❌ Problem: "Changes not visible after merge"

**Cause**: Dependencies not installed (node_modules not in git)

**Solution**:
```bash
cd frontend
npm install
npm start
```

📖 **Full troubleshooting guide**: See [`DEPLOYMENT_GUIDE.md`](./DEPLOYMENT_GUIDE.md)

### ❌ Problem: "Map not displaying"

**Solutions**:
1. Verify `npm install` completed successfully
2. Hard refresh browser: `Ctrl+Shift+F5`
3. Check browser console (F12) for errors
4. Clear Angular cache: `rm -rf frontend/.angular/cache`

### ❌ Problem: "Backend API errors"

**Solutions**:
1. Verify database connection in `appsettings.json`
2. Check if database migrations are up to date
3. Ensure .NET SDK version is compatible
4. Check backend logs for detailed error messages

---

## Development Workflow

### Making Changes

1. **Create a feature branch**:
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes** in frontend or backend

3. **Test locally**:
   ```bash
   # Frontend
   cd frontend && npm start
   
   # Backend
   cd backend && dotnet run
   ```

4. **Commit and push**:
   ```bash
   git add .
   git commit -m "Description of changes"
   git push origin feature/your-feature-name
   ```

5. **Create a Pull Request** on GitHub

### After Merging to Main

**Everyone on the team must run**:
```bash
git pull origin main
cd frontend && npm install    # Install any new dependencies
cd ../backend && dotnet restore
```

---

## Project Features

### Frontend Features
- ✅ Time tracking (Pointage)
- ✅ Circuit management with map integration
- ✅ User authentication and authorization
- ✅ Responsive design (mobile-friendly)
- ✅ Real-time data updates
- ✅ Advanced data grids with filtering/sorting
- ✅ PDF report generation
- ✅ Multi-language support (i18n)

### Backend Features
- ✅ RESTful API
- ✅ JWT authentication
- ✅ Entity Framework Core ORM
- ✅ Clean Architecture pattern
- ✅ Swagger API documentation
- ✅ Database migrations

---

## Documentation

- [`DEPLOYMENT_GUIDE.md`](./DEPLOYMENT_GUIDE.md) - Complete deployment and troubleshooting guide
- [`MAP_FEATURE_SETUP.md`](./MAP_FEATURE_SETUP.md) - Detailed map feature documentation
- [`frontend/README.md`](./frontend/README.md) - Frontend-specific documentation
- [`backend/README.md`](./backend/README.md) - Backend-specific documentation

---

## Environment Variables

### Frontend
Create `frontend/src/environments/environment.ts`:
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

### Backend
Update `backend/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PointagePfe;..."
  },
  "JwtSettings": {
    "Secret": "your-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience"
  }
}
```

---

## Testing

### Frontend Tests
```bash
cd frontend
npm test              # Run unit tests
npm run e2e          # Run end-to-end tests (if configured)
```

### Backend Tests
```bash
cd backend
dotnet test          # Run all tests
```

---

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Write/update tests
5. Ensure all tests pass
6. Submit a pull request

---

## License

See [LICENSE.md](./LICENSE.md) for details.

---

## Support

For issues or questions:
1. Check the [DEPLOYMENT_GUIDE.md](./DEPLOYMENT_GUIDE.md) for common problems
2. Open an issue on GitHub
3. Contact the development team

---

## Quick Reference Commands

```bash
# Clone repository
git clone https://github.com/rayen103/PointagePfe.git

# Setup everything
cd PointagePfe/frontend && npm install
cd ../backend && dotnet restore

# Run frontend
cd frontend && npm start

# Run backend
cd backend && dotnet run --project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj

# After pulling changes
git pull && cd frontend && npm install && cd ../backend && dotnet restore
```

---

**Remember**: Always run `npm install` and `dotnet restore` after pulling changes! 🚀
