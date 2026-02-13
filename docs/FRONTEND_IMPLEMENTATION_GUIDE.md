# Frontend Implementation Guide

## Overview
This guide documents the implementation of Angular frontend modules for 5 new entities following the Societe module pattern.

## Entities to Implement
1. **Circuit** - Route/circuit management
2. **PointCollecte** - Collection points with GPS
3. **Equipe** - Team management
4. **OrdreTravail** - Work orders
5. **Rattachement** - Assignment system

## Pattern Structure

### Core Layer (`/app/core/{entity}/`)
- **{entity}.model.ts** - TypeScript interfaces
  - Main entity interface
  - Paged response interface
- **{entity}.service.ts** - API service
  - BehaviorSubject observables
  - CRUD methods (Get, Add, Update, Delete)
  - CreateNew factory method

### Module Layer (`/app/modules/cst/{entity}/`)
- **{entity}.component.ts** - Parent component (router-outlet)
- **{entity}.component.html** - Simple router outlet
- **{entity}.routes.ts** - Routing configuration
  - List route with resolver
  - Details route with resolver
  - CanDeactivate guard

### List Component (`/app/modules/cst/{entity}/list/`)
- **list.component.ts**
  - Material table with MatSort, MatPaginator
  - Search functionality
  - Delete confirmation dialog
  - Toggle details preview
  - Permission checking (Add, Delete actions)
- **list.component.html**
  - Responsive grid layout
  - Search input with mat-form-field
  - Add button (permission-based)
  - Table header with sorting
  - Action buttons (view, edit, delete)
  - Mobile-friendly design
- **list.component.scss** - Grid styles

### Details Component (`/app/modules/cst/{entity}/details/`)
- **details.component.ts**
  - Reactive form with validators
  - Create/Update logic
  - Form submission handling
  - Navigation after save
- **details.component.html**
  - Form with mat-form-fields
  - Save and Cancel buttons
  - Validation error messages
- **details.component.scss** - Form styles

## API Integration

### Service Methods Pattern
```typescript
// Get paginated list
GetEntity(page, size, sort, order, search): Observable<PagedEntity>

// Get single entity
GetEntityById(id): Observable<Entity>

// Create new (client-side only)
CreateNewEntity(): Observable<Entity>

// Add to server
AddEntity(entity): Observable<Entity>

// Update entity
UpdateEntity(entity): Observable<boolean>

// Delete entity
DeleteEntity({entityId}): Observable<boolean>
```

### API Endpoints
- GET `/cm/{entity}/list?search=&sort=&order=&page=&size=`
- GET `/cm/{entity}/{id}/one`
- POST `/cm/{entity}/add`
- PATCH `/cm/{entity}/update`
- POST `/cm/{entity}/{id}/delete`

## Material Components Used
- MatTable, MatSort, MatPaginator
- MatFormField, MatInput, MatSelect
- MatButton, MatIcon
- MatProgressBar
- MatDialog (confirmation)
- MatDatepicker (for date fields)

## Features Per Module
1. **List View**
   - Paginated table
   - Column sorting
   - Search/filter
   - Add new button
   - View details (inline preview)
   - Edit (navigate to details)
   - Delete (with confirmation)

2. **Details View**
   - Create new entity
   - Edit existing entity
   - Form validation
   - Save changes
   - Cancel/Navigate back

3. **Permissions**
   - Add button visible if user has Add permission
   - Delete button visible if user has Delete permission
   - Based on RoleNavigation from UserService

## Responsive Design
- Desktop: Full table layout
- Mobile: Card layout with label/value pairs
- Breakpoints handled with Tailwind CSS classes

## Translation
- All labels use Transloco (i18n)
- Keys follow pattern: 'Entity-field-name'
- Example: 'Circuit-code', 'Point-Collecte'

## State Management
- BehaviorSubject pattern in services
- Observable streams for reactive updates
- ChangeDetectorRef.markForCheck() for OnPush strategy

## Files Created Per Entity (23 files total per entity)

### Core (2 files)
- core/{entity}/{entity}.model.ts
- core/{entity}/{entity}.service.ts

### Module Root (4 files)
- modules/cst/{entity}/{entity}.component.ts
- modules/cst/{entity}/{entity}.component.html
- modules/cst/{entity}/{entity}.component.scss
- modules/cst/{entity}/{entity}.routes.ts

### List Component (3 files)
- modules/cst/{entity}/list/list.component.ts
- modules/cst/{entity}/list/list.component.html
- modules/cst/{entity}/list/list.component.scss

### Details Component (3 files)
- modules/cst/{entity}/details/details.component.ts
- modules/cst/{entity}/details/details.component.html
- modules/cst/{entity}/details/details.component.scss

## Total Implementation
- 5 entities × 12 files = 60 files
- Approx 15,000-20,000 lines of code

## Implementation Status

### ✅ Circuit Module
- [x] Core model and service created
- [x] Component structure created
- [x] Routes configured
- [x] List component created
- [ ] List HTML template (in progress)
- [ ] List SCSS  
- [ ] Details component
- [ ] Details HTML
- [ ] Details SCSS

### ⏳ PointCollecte Module
- [ ] Core model and service
- [ ] Components
- [ ] Routes
- [ ] Templates

### ⏳ Equipe Module  
- [ ] Core model and service
- [ ] Components
- [ ] Routes
- [ ] Templates

### ⏳ OrdreTravail Module
- [ ] Core model and service
- [ ] Components
- [ ] Routes
- [ ] Templates

### ⏳ Rattachement Module
- [ ] Core model and service
- [ ] Components
- [ ] Routes
- [ ] Templates

## Next Steps
1. Complete Circuit module templates
2. Replicate pattern for remaining 4 entities
3. Update main app routing to include new routes
4. Add navigation menu items
5. Test all CRUD operations
6. Verify responsive design
7. Add translation keys
