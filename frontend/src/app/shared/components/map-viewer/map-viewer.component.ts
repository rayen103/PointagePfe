import {
    AfterViewInit,
    ChangeDetectionStrategy,
    Component,
    Input,
    OnChanges,
    OnDestroy,
    SimpleChanges,
    ViewEncapsulation,
} from '@angular/core';
import * as L from 'leaflet';
import { Map, Marker } from 'leaflet';

export interface MapLocation {
    id: string;
    circuitId?: string;
    name: string;
    latitude: number;
    longitude: number;
    isActive?: boolean;
    description?: string;
}

@Component({
    selector: 'app-map-viewer',
    standalone: true,
    templateUrl: './map-viewer.component.html',
    styleUrl: './map-viewer.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MapViewerComponent implements AfterViewInit, OnChanges, OnDestroy {
    @Input() locations: MapLocation[] = [];
    @Input() height: string = '600px';
    @Input() zoom: number = 7;

    private map: Map | null = null;
    private markers: Marker[] = [];

    ngAfterViewInit(): void {
        this.initMap();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['locations'] && !changes['locations'].firstChange && this.map) {
            this.updateMarkers();
        }
    }

    private initMap(): void {
        // Create the map centered on Tunisia
        const center = L.latLng(36.8065, 10.1815); // Tunis, Tunisia

        this.map = L.map('map-viewer', {
            center: center,
            zoom: this.zoom,
        });

        // Add OpenStreetMap tile layer
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '© OpenStreetMap contributors',
            maxZoom: 19,
        }).addTo(this.map);

        // Add markers for locations
        this.updateMarkers();
    }

    private updateMarkers(): void {
        if (!this.map) {
            return;
        }

        // Clear existing markers
        this.markers.forEach(marker => marker.remove());
        this.markers = [];

        // Filter locations with valid coordinates
        const validLocations = this.locations.filter(
            loc => loc.latitude != null && loc.longitude != null
        );

        if (validLocations.length === 0) {
            return;
        }

        // Add markers for each location
        validLocations.forEach(location => {
            const icon = this.createMarkerIcon(location.isActive);
            
            const marker = L.marker([location.latitude, location.longitude], { icon })
                .addTo(this.map!)
                .bindPopup(this.createPopupContent(location));

            this.markers.push(marker);
        });

        // Fit map bounds to show all markers
        if (validLocations.length > 0) {
            const bounds = L.latLngBounds(
                validLocations.map(loc => [loc.latitude, loc.longitude] as [number, number])
            );
            this.map.fitBounds(bounds, { padding: [50, 50], maxZoom: 12 });
        }
    }

    private createMarkerIcon(isActive?: boolean): L.Icon {
        // Color-code markers based on active status
        const color = isActive === false ? 'red' : 'green';
        
        // Use different marker colors
        const iconUrl = isActive === false
            ? 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png'
            : 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png';

        return L.icon({
            iconUrl: iconUrl,
            shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            shadowSize: [41, 41],
        });
    }

    private createPopupContent(location: MapLocation): string {
        const status = location.isActive !== undefined
            ? `<div class="font-semibold ${location.isActive ? 'text-green-600' : 'text-red-600'}">${location.isActive ? 'Active' : 'Inactive'}</div>`
            : '';
        
        const description = location.description
            ? `<div class="text-sm text-gray-600">${location.description}</div>`
            : '';

        return `
            <div class="p-2">
                <div class="font-bold text-lg mb-1">${location.name}</div>
                ${status}
                ${description}
                <div class="text-xs text-gray-500 mt-2">
                    ${location.latitude.toFixed(6)}, ${location.longitude.toFixed(6)}
                </div>
            </div>
        `;
    }

    ngOnDestroy(): void {
        if (this.map) {
            this.map.remove();
            this.map = null;
        }
    }
}
