# Map Feature Setup - Quick Reference

## What Changed in the Recent Merge

The merge added **interactive map functionality** to the Circuit module using Leaflet.js.

---

## Required Steps to See the Changes

### Step 1: Install Dependencies

```bash
cd frontend
npm install
```

This installs the new map-related packages:
- `leaflet` - Core mapping library
- `@asymmetrik/ngx-leaflet` - Angular wrapper for Leaflet
- `@types/leaflet` - TypeScript definitions
- `leaflet-color-markers` - Colored map markers
- `leaflet-routing-machine` - Route planning (if used)

### Step 2: Start the Development Server

```bash
npm start
```

### Step 3: View the Map Feature

1. Open browser: `http://localhost:4200`
2. Navigate to: **Admin → Circuits**
3. Click **Create New Circuit** or edit an existing circuit
4. Scroll down to see the **interactive map**
5. Click or drag the marker to set the location

---

## Map Components Added

### 1. MapPickerComponent
**Location**: `frontend/src/app/shared/components/map-picker/`

**Purpose**: Interactive map for selecting circuit locations

**Features**:
- Click anywhere to place marker
- Drag marker to update location
- Shows latitude/longitude coordinates
- Centered on Tunisia by default (36.8065°N, 10.1815°E)
- Emits location changes to parent component

**Usage in Forms**:
```html
<app-map-picker
  [latitude]="form.value.latitude"
  [longitude]="form.value.longitude"
  (locationChange)="onLocationChange($event)">
</app-map-picker>
```

### 2. MapViewerComponent
**Location**: `frontend/src/app/shared/components/map-viewer/`

**Purpose**: Display read-only map with multiple locations

**Features**:
- Shows multiple circuits on one map
- Color-coded markers:
  - 🟢 Green = Active circuit
  - 🔴 Red = Inactive circuit
- Popup info windows with circuit details
- Auto-fits bounds to show all markers
- Displays location name, coordinates, and status

**Usage in Lists**:
```html
<app-map-viewer
  [locations]="circuits"
  [height]="'500px'">
</app-map-viewer>
```

---

## Configuration Details

### CSS Already Configured
The Leaflet CSS is already included in `angular.json`:
```json
"styles": [
  "node_modules/leaflet/dist/leaflet.css",
  "node_modules/leaflet-routing-machine/dist/leaflet-routing-machine.css"
]
```

### TypeScript Already Configured
The Leaflet types are already in `package.json` devDependencies.

### No API Key Required
The implementation uses **OpenStreetMap** tiles, which are free and don't require an API key!

---

## Verifying the Installation

### Check 1: Dependencies Installed
```bash
cd frontend
ls node_modules/leaflet
```
Should show the leaflet package directory.

### Check 2: Components Exist
```bash
ls src/app/shared/components/map-picker/
ls src/app/shared/components/map-viewer/
```
Should show the TypeScript, HTML, and SCSS files.

### Check 3: Browser Console
Open browser console (F12) and look for:
- ✅ No Leaflet errors
- ✅ Map tiles loading
- ❌ Any CORS errors or 404s

---

## Common Issues & Quick Fixes

### Issue: Map shows but tiles don't load
**Cause**: Network/CORS issues with OpenStreetMap
**Fix**: Check internet connection, try refreshing

### Issue: Map area is blank
**Cause**: CSS not loaded or container height not set
**Fix**: 
1. Verify `npm install` completed successfully
2. Hard refresh browser (Ctrl+Shift+F5)
3. Check that map container has height in CSS

### Issue: Marker doesn't appear
**Cause**: Default marker icon path issues
**Fix**: Already handled in the component with explicit icon configuration

### Issue: Cannot drag marker
**Cause**: Marker not set to draggable
**Fix**: Already configured with `draggable: true` in MapPickerComponent

---

## Testing the Feature

### Test Scenario 1: Create Circuit with Location
1. Go to Circuits → Create New
2. Fill in circuit details
3. Click on the map to place a marker
4. Verify latitude/longitude fields update
5. Save the circuit
6. Edit the circuit - marker should appear at saved location

### Test Scenario 2: Drag Marker
1. Edit an existing circuit
2. Drag the marker to a new position
3. Verify coordinates update in real-time
4. Save and verify new location persists

### Test Scenario 3: View Multiple Circuits on Map
1. Go to Circuits list
2. If MapViewerComponent is implemented in the list view
3. Verify all circuits appear as markers
4. Click markers to see popup info

---

## Data Model Changes

The Circuit model now includes:

```typescript
interface Circuit {
  // ... existing fields ...
  latitude?: number;
  longitude?: number;
}
```

These fields are:
- Optional (can be null/undefined)
- Stored as decimal numbers
- Validated in the form (must be valid coordinates if provided)

---

## Performance Notes

- Maps lazy-load tiles as needed
- Marker rendering is efficient for up to ~1000 markers
- Components use OnPush change detection for performance
- No memory leaks - proper cleanup in ngOnDestroy

---

## Browser Compatibility

Works in all modern browsers:
- ✅ Chrome/Edge 90+
- ✅ Firefox 88+
- ✅ Safari 14+
- ✅ Mobile browsers (iOS Safari, Chrome Mobile)

---

## What If I Don't See Changes?

Run this complete reset:

```bash
# Stop the dev server (Ctrl+C)

cd frontend

# Clean everything
rm -rf node_modules package-lock.json .angular

# Fresh install
npm install

# Start server
npm start

# Hard refresh browser (Ctrl+Shift+F5)
```

---

## Summary

**Why you don't see changes**: New dependencies need to be installed!

**Solution**: 
```bash
cd frontend && npm install && npm start
```

**Then**: Go to Circuits module and create/edit a circuit to see the interactive map!

The map feature is fully implemented and ready to use - you just need to install the dependencies first! 🗺️
