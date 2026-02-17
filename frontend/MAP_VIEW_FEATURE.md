# Map View Feature for Circuit List

## Overview
Added an interactive map view to display all circuit locations when viewing the circuit list. Users can now toggle between a traditional list view and a map view showing all circuits with location data.

## Features Implemented

### 1. MapViewerComponent
A new reusable component for displaying multiple locations on a map:
- **Location**: `frontend/src/app/shared/components/map-viewer/`
- **Functionality**:
  - Displays multiple circuit markers on OpenStreetMap
  - Color-coded markers (green = active circuits, red = inactive circuits)
  - Interactive popups showing circuit details on marker click
  - Auto-fits map bounds to show all circuit markers
  - Tunisia-centered default view (36.8065°N, 10.1815°E)

### 2. Circuit List Enhancements
Updated the circuit list component to support map view:
- **Toggle Buttons**: Switch between List and Map views
- **Information Banner**: Shows count of circuits with/without location data
- **Filtered Display**: Only circuits with valid latitude/longitude appear on map
- **Responsive Design**: Works on both desktop and mobile devices

## How to Test

### Prerequisites
1. Log in to the application at http://localhost:4200
2. Navigate to the Circuits section

### Testing Steps

#### 1. View Toggle
- On the Circuit List page, look for the toggle buttons in the header
- Two buttons will be visible: **List** (default) and **Map**
- Click the **Map** button to switch to map view
- Click the **List** button to return to list view

#### 2. Map View
When in map view, you should see:
- An interactive OpenStreetMap centered on Tunisia
- Markers for all circuits that have latitude/longitude data
- An information banner showing statistics (e.g., "Showing 5 circuits with location (2 without location)")

#### 3. Marker Interactions
- **Green Markers**: Active circuits
- **Red Markers**: Inactive circuits
- **Click a marker**: A popup will appear showing:
  - Circuit code and label
  - Status (Active/Inactive)
  - Description
  - Exact coordinates

#### 4. Map Behavior
- The map automatically adjusts to show all circuit markers
- If no circuits have location data, a message displays: "No circuits with location found"
- Zoom in/out using mouse wheel or map controls
- Pan the map by clicking and dragging

#### 5. Search and Filter
- Search functionality works in both list and map view
- When you search, the map updates to show only matching circuits

## Visual Reference

### List View (Existing)
The traditional table/grid view showing all circuit information.

### Map View (New)
![Map View](https://github.com/user-attachments/assets/29d7b936-9556-400d-9d18-4bc8695d892f)
*Interactive map showing circuit locations across Tunisia*

## Technical Details

### Component Structure
```typescript
MapViewerComponent
- Input: locations: MapLocation[]
- Input: height: string (default: '600px')
- Input: zoom: number (default: 7)
- Uses Leaflet for map rendering
- Implements OnChanges for dynamic updates
```

### MapLocation Interface
```typescript
interface MapLocation {
    id: string;
    name: string;
    latitude: number;
    longitude: number;
    isActive?: boolean;
    description?: string;
}
```

### Files Modified/Created
1. **New Component**: `frontend/src/app/shared/components/map-viewer/`
   - `map-viewer.component.ts`
   - `map-viewer.component.html`
   - `map-viewer.component.scss`

2. **Modified**: `frontend/src/app/modules/cst/circuit/list/`
   - `list.component.ts` - Added map view logic
   - `list.component.html` - Added toggle and map display

## Known Limitations

1. **Location Data Required**: Circuits without latitude/longitude won't appear on the map
2. **Migration Required**: The database migration must be applied before circuits can have location data (see MIGRATION_INSTRUCTIONS.md)
3. **Authentication**: Feature requires user authentication to access

## Testing with Sample Data

To fully test the feature, you need circuits with location data:

1. **Apply the database migration** (if not already done):
   ```bash
   cd backend
   dotnet ef database update
   ```

2. **Add/Edit circuits with location data**:
   - Go to Circuits → Add Circuit or edit existing
   - Scroll to "Circuit Location" section
   - Click on the map or enter coordinates manually
   - Save the circuit

3. **View circuits on the map**:
   - Go to Circuit List
   - Click the **Map** toggle button
   - See your circuits displayed on the map

## Sample Tunisia Coordinates for Testing

Here are some coordinates in Tunisia for testing:
- **Tunis** (Capital): 36.8065°N, 10.1815°E
- **Sfax**: 34.7406°N, 10.7603°E
- **Sousse**: 35.8256°N, 10.6369°E
- **Kairouan**: 35.6781°N, 10.0963°E
- **Bizerte**: 37.2744°N, 9.8739°E
- **Gabès**: 33.8815°N, 10.0982°E

## Future Enhancements (Optional)

1. **Marker Clustering**: Group nearby markers when zoomed out
2. **Directions**: Show routes between circuits
3. **Heatmap**: Visualize circuit density
4. **Export**: Download map as image
5. **Print**: Print-friendly map view
6. **Custom Markers**: Different icons for circuit types
7. **Filter by Region**: Show only circuits in specific regions

## Troubleshooting

### Map doesn't display
- Check browser console for errors
- Verify Leaflet CSS is loaded (should be in index.html)
- Ensure circuits have valid latitude/longitude values

### Markers don't appear
- Verify circuits have non-null latitude/longitude in database
- Check browser network tab for blocked requests
- Look for JavaScript errors in console

### Toggle doesn't work
- Check that MapViewerComponent is properly imported
- Verify Angular change detection is working
- Check for console errors

## Support

For issues or questions:
- Check browser console for error messages
- Verify database migration was applied successfully
- Ensure circuits have location data
- Review Leaflet documentation: https://leafletjs.com/
