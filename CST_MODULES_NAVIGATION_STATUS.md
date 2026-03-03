# CST Modules Navigation Status - Complete Guide

## Current Status: All CST Modules Are Configured ✅

All interfaces/modules under `@cst/` (which maps to `frontend/src/app/modules/cst/`) are **already properly configured** in the navigation menu.

---

## 📋 Configured CST Modules

All 6 modules under the CST directory are fully integrated:

### 1. Societe 🏢
- **Route**: `/fichier/societe`
- **Navigation ID**: `fichier.societe`
- **Icon**: `mat_outline:business`
- **Location**: `modules/cst/societe/`
- **Status**: ✅ In menu

### 2. Circuit 🛣️
- **Route**: `/fichier/circuit`
- **Navigation ID**: `fichier.circuit`
- **Icon**: `mat_outline:alt_route`
- **Location**: `modules/cst/circuit/`
- **Status**: ✅ In menu

### 3. Point de Collecte 📍
- **Route**: `/fichier/pointcollecte`
- **Navigation ID**: `fichier.pointcollecte`
- **Icon**: `mat_outline:location_on`
- **Location**: `modules/cst/pointcollecte/`
- **Status**: ✅ In menu

### 4. Equipe 👥
- **Route**: `/fichier/equipe`
- **Navigation ID**: `fichier.equipe`
- **Icon**: `mat_outline:groups`
- **Location**: `modules/cst/equipe/`
- **Status**: ✅ In menu

### 5. Ordre de Travail 📋
- **Route**: `/fichier/ordretravail`
- **Navigation ID**: `fichier.ordretravail`
- **Icon**: `mat_outline:assignment`
- **Location**: `modules/cst/ordretravail/`
- **Status**: ✅ In menu

### 6. Rattachement 🔗
- **Route**: `/fichier/rattachement`
- **Navigation ID**: `fichier.rattachement`
- **Icon**: `mat_outline:link`
- **Location**: `modules/cst/rattachement/`
- **Status**: ✅ In menu

---

## 🔍 Why You Might Not See Your Module

If you created a new interface/module but don't see it in the menu, here are possible reasons:

### 1. **Module Not Yet Created Properly**
You may have created TypeScript interface files but not a complete Angular module with:
- Component files (`.component.ts`, `.component.html`)
- Route configuration (`.routes.ts`)
- Required services

**Solution**: Create a full module structure like the existing ones.

### 2. **Routes Not Configured**
The module exists but hasn't been added to `app.routes.ts`.

**Solution**: Add route configuration (see "How to Add a New Module" below).

### 3. **Navigation Item Missing**
Routes are configured but navigation menu item is missing from `navigation.data.ts`.

**Solution**: Add navigation item (see "How to Add a New Module" below).

### 4. **Browser Cache Issue**
Your changes exist but the browser is showing cached content.

**Solution**:
```bash
# Clear browser cache (Ctrl+Shift+Delete)
# Or hard refresh (Ctrl+Shift+R / Cmd+Shift+R)
# Or restart dev server:
cd frontend
npm start
```

### 5. **Permission Issue**
The module requires specific permissions your user doesn't have.

**Solution**: Check user permissions in the database or ask admin to grant access to the navigationId.

### 6. **Dev Server Not Restarted**
Changes made but dev server wasn't restarted.

**Solution**:
```bash
cd frontend
# Stop current server (Ctrl+C)
npm start
```

---

## 🚀 How to Add a New CST Module to Navigation

If you want to add a **new** module under @cst/, follow these steps:

### Step 1: Create Module Structure

```bash
cd frontend/src/app/modules/cst
mkdir my-new-module
cd my-new-module
```

Create these files:
- `my-new-module.component.ts`
- `my-new-module.component.html`
- `my-new-module.component.scss`
- `my-new-module.routes.ts`
- `list/list.component.ts` (for list view)
- `details/details.component.ts` (for details view)

### Step 2: Configure Routes in `app.routes.ts`

Add to the `fichier` children array (around line 102-130):

```typescript
{
    path: 'my-new-module',
    data: { navigationId: 'fichier.my-new-module' },
    loadChildren: () => import('./modules/cst/my-new-module/my-new-module.routes')
},
```

### Step 3: Add Navigation Item in `navigation.data.ts`

Add to the `fichier` children array in `defaultNavigation` (around line 11-85):

```typescript
{
    id   : 'fichier.my-new-module',
    title: 'My New Module',
    type : 'basic',
    icon : 'mat_outline:featured_play_list', // Choose appropriate icon
    link : '/fichier/my-new-module',
    action:[FuseNavigationAction.Add, FuseNavigationAction.Edit, FuseNavigationAction.Delete]
},
```

### Step 4: Create Route Configuration

In `my-new-module.routes.ts`:

```typescript
import { Routes } from '@angular/router';
import { MyNewModuleComponent } from './my-new-module.component';
import { ListComponent } from './list/list.component';
import { DetailsComponent } from './details/details.component';

export default [
    {
        path: '',
        component: MyNewModuleComponent,
        children: [
            {
                path: '',
                component: ListComponent,
                title: 'My New Module List',
            },
            {
                path: ':id',
                component: DetailsComponent,
                title: 'My New Module Details',
            }
        ]
    }
] as Routes;
```

### Step 5: Test

```bash
# Restart dev server
cd frontend
npm start

# Open browser to http://localhost:4200
# Check sidebar menu - your new module should appear
# Click it to navigate
```

---

## 🎨 Available Material Icons

Choose from these Material Design icons for your module:

- `mat_outline:featured_play_list` - Featured list
- `mat_outline:view_list` - List view
- `mat_outline:dashboard` - Dashboard
- `mat_outline:settings` - Settings
- `mat_outline:analytics` - Analytics
- `mat_outline:inventory` - Inventory
- `mat_outline:category` - Category
- `mat_outline:work` - Work
- `mat_outline:description` - Description
- `mat_outline:folder` - Folder

More icons: https://fonts.google.com/icons

---

## 📂 File Locations

### Navigation Configuration
- **File**: `frontend/src/app/core/navigation/navigation.data.ts`
- **Section**: `defaultNavigation` → `fichier` → `children`
- **Lines**: Approximately 11-85

### Route Configuration
- **File**: `frontend/src/app/app.routes.ts`
- **Section**: Under `fichier` children
- **Lines**: Approximately 102-130

### Module Directory
- **Location**: `frontend/src/app/modules/cst/`
- **Alias**: `@cst/` in tsconfig paths

---

## 🔧 Troubleshooting Commands

### Check if module files exist
```bash
cd frontend/src/app/modules/cst
ls -la
```

### Verify routes configuration
```bash
grep -n "my-module-name" frontend/src/app/app.routes.ts
```

### Verify navigation item
```bash
grep -n "my-module-name" frontend/src/app/core/navigation/navigation.data.ts
```

### Check TypeScript compilation
```bash
cd frontend
npm run build
```

### Clear and rebuild
```bash
cd frontend
rm -rf .angular node_modules
npm install
npm start
```

---

## ✅ Verification Checklist

Before asking why a module isn't in the menu, verify:

- [ ] Module directory exists in `modules/cst/`
- [ ] `.routes.ts` file exists and is properly configured
- [ ] Route added to `app.routes.ts` with correct path and navigationId
- [ ] Navigation item added to `navigation.data.ts` with matching navigationId
- [ ] Dev server restarted after changes
- [ ] Browser cache cleared or hard refresh performed
- [ ] User has permission for the navigationId (if permissions are enforced)
- [ ] No TypeScript compilation errors
- [ ] Console shows no errors (F12 → Console)

---

## 📊 Current Navigation Menu Structure

```
📁 Fichier
   ├─ 🏢 Societe                    ← CST module ✅
   ├─ 👥 Utilisateur
   ├─ 🎫 Employe
   ├─ 👤 Role
   ├─ 🛣️  Circuit                   ← CST module ✅
   ├─ 📍 Point de Collecte         ← CST module ✅
   ├─ 👥 Equipe                    ← CST module ✅
   ├─ 📋 Ordre de Travail          ← CST module ✅
   └─ 🔗 Rattachement              ← CST module ✅
```

---

## 🆘 Need Help?

If your module still doesn't appear:

1. **Check the exact module name** you created
2. **Verify file structure** matches existing modules
3. **Look for console errors** in browser (F12)
4. **Check terminal output** for compilation errors
5. **Review permissions** if navigation guard is enforcing them

### Common Mistakes

❌ Created only interface files (`.ts` with `interface` keyword)
✅ Need full Angular module with components

❌ Route path doesn't match directory name
✅ Keep them consistent

❌ navigationId in routes doesn't match id in navigation.data.ts
✅ Must be exactly the same

❌ Forgot to import/export components
✅ Ensure all imports are correct

---

## 📖 Related Documentation

- [COMPLETE_FIX_SUMMARY.md](COMPLETE_FIX_SUMMARY.md) - Navigation system overview
- [NAVIGATION_FIX.md](NAVIGATION_FIX.md) - Navigation configuration details
- [ALL_MODULES_NAVIGATION.md](ALL_MODULES_NAVIGATION.md) - Complete module list

---

**Status**: All CST modules currently in the repository are properly configured ✅

**Action Needed**: Please specify which **specific module/interface** you created that's not appearing in the menu, and we can add it following the steps above.
