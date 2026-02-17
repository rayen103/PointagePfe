# Circuit List - Map View Feature

## UI Layout

```
┌─────────────────────────────────────────────────────────────────┐
│  Circuits                                        [Search...] [+] │
│  ┌──────────────┐                                                │
│  │ List │ Map  │  ← Toggle Buttons                               │
│  └──────────────┘                                                │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  LIST VIEW (Default)                                            │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │ Code    │ Label      │ Description  │ Status  │ Actions   │ │
│  ├────────────────────────────────────────────────────────────┤ │
│  │ C001    │ Circuit 1  │ Main route   │ Active  │ 👁 ✏ 🗑    │ │
│  │ C002    │ Circuit 2  │ Secondary    │ Inactive│ 👁 ✏ 🗑    │ │
│  │ C003    │ Circuit 3  │ Branch       │ Active  │ 👁 ✏ 🗑    │ │
│  └────────────────────────────────────────────────────────────┘ │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## Map View (When Map Button Clicked)

```
┌─────────────────────────────────────────────────────────────────┐
│  Circuits                                        [Search...] [+] │
│  ┌──────────────┐                                                │
│  │ List │ Map  │  ← Map button is highlighted                   │
│  └──────────────┘                                                │
├─────────────────────────────────────────────────────────────────┤
│                                                                  │
│  ℹ️ Showing 3 circuits with location (1 without location)       │
│                                                                  │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │                    🗺️ TUNISIA MAP                          │ │
│  │                                                            │ │
│  │           📍 (Green)  Circuit 1 - Active                   │ │
│  │                       ┌─────────────────┐                 │ │
│  │                       │ Circuit 1       │                 │ │
│  │                       │ Status: Active  │                 │ │
│  │                       │ Main route      │                 │ │
│  │                       │ 36.81°N, 10.18°E│                 │ │
│  │                       └─────────────────┘                 │ │
│  │                                                            │ │
│  │      📍 (Red)   Circuit 2 - Inactive                       │ │
│  │                                                            │ │
│  │                            📍 (Green) Circuit 3           │ │
│  │                                                            │ │
│  │                                                            │ │
│  │  [+ - Zoom Controls]                                      │ │
│  │                                                            │ │
│  └────────────────────────────────────────────────────────────┘ │
│                                                                  │
└─────────────────────────────────────────────────────────────────┘
```

## Marker Color Legend

- 📍 **Green Marker**: Active Circuit
- 📍 **Red Marker**: Inactive Circuit

## Popup Content (On Marker Click)

```
┌─────────────────────┐
│ Circuit C001        │
│ Circuit Label       │
│ ─────────────────── │
│ Status: Active ✓    │
│ Description text... │
│ ─────────────────── │
│ 36.8065°N, 10.1815°E│
└─────────────────────┘
```

## Mobile View

```
┌──────────────────────┐
│ Circuits        [+]  │
│ ┌────────┬────────┐  │
│ │  List  │  Map   │  │
│ └────────┴────────┘  │
│ [Search............] │
├──────────────────────┤
│                      │
│ ℹ️ Showing 3 circuits│
│    with location     │
│                      │
│ ┌──────────────────┐ │
│ │   🗺️ MAP VIEW   │ │
│ │                  │ │
│ │    📍 📍 📍      │ │
│ │                  │ │
│ │  [+] [-]         │ │
│ └──────────────────┘ │
│                      │
└──────────────────────┘
```

## Component Hierarchy

```
ListComponent
├── Header
│   ├── Title: "Circuits"
│   ├── Toggle Buttons
│   │   ├── List Button
│   │   └── Map Button
│   ├── Search Field
│   └── Add Button
│
├── Map View (conditional: *ngIf="showMapView")
│   ├── Info Banner
│   │   └── Circuit count statistics
│   └── MapViewerComponent
│       ├── Leaflet Map
│       ├── OSM Tile Layer
│       └── Markers[]
│           ├── Marker (green/red)
│           └── Popup
│               ├── Circuit Name
│               ├── Status
│               ├── Description
│               └── Coordinates
│
└── List View (conditional: *ngIf="!showMapView")
    ├── Data Grid
    │   ├── Headers
    │   └── Rows[]
    │       ├── Circuit Data
    │       └── Action Buttons
    └── Paginator
```

## Data Flow

```
CircuitService.circuits$
        ↓
   ListComponent
        ↓
   circuits[] ──→ updateMapLocations()
        ↓
   mapLocations[]
        ↓
   MapViewerComponent
        ↓
   Leaflet Map with Markers
```

## State Management

```typescript
// ListComponent Properties
showMapView: boolean = false;  // Toggle state
mapLocations: MapLocation[] = [];  // Filtered data for map

// Toggle Action
toggleMapView() {
    this.showMapView = !this.showMapView;
}

// Data Transformation
private updateMapLocations(circuits: Circuit[]): void {
    this.mapLocations = circuits
        .filter(c => c.latitude != null && c.longitude != null)
        .map(c => ({
            id: c.circuitId,
            name: c.codeCircuit + ' - ' + c.libelleCircuit,
            latitude: c.latitude,
            longitude: c.longitude,
            isActive: c.isActive,
            description: c.description
        }));
}
```

## User Interaction Flow

```
1. User lands on Circuit List
   └─> Default: List View displayed

2. User clicks "Map" button
   ├─> showMapView = true
   ├─> MapViewerComponent renders
   ├─> mapLocations filtered and passed
   └─> Map displays with markers

3. User clicks on a marker
   ├─> Popup opens
   └─> Shows circuit details

4. User clicks "List" button
   ├─> showMapView = false
   └─> Returns to list view
```

## Feature Benefits

✅ **Visual Context**: See geographical distribution of circuits
✅ **Quick Overview**: Identify circuit locations at a glance  
✅ **Status Indication**: Color-coded markers show active/inactive
✅ **Detailed Info**: Click markers for circuit details
✅ **Flexible View**: Easy toggle between list and map
✅ **Search Integration**: Map updates with search results
✅ **Mobile Friendly**: Responsive design for all devices
