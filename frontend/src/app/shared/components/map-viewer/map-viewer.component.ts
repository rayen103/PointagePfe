import {
    AfterViewInit,
    ChangeDetectionStrategy,
    Component,
    EventEmitter,
    Input,
    OnChanges,
    OnDestroy,
    Output,
    SimpleChanges,
    ViewEncapsulation,
} from '@angular/core';
import * as L from 'leaflet';
import { Map as LeafletMap, Marker, Polyline } from 'leaflet';
import 'leaflet-routing-machine';

export type MapPointType = 'base' | 'departure' | 'arrival';
const TUNISIA_BOUNDS = L.latLngBounds(
    [30.1, 7.5],
    [37.6, 11.8]
);

export interface MapLocation {
    id: string;
    circuitId?: string;
    pointType?: MapPointType;
    name: string;
    latitude: number;
    longitude: number;
    isActive?: boolean;
    description?: string;
    color?: string;
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
    @Output() readonly mapClick = new EventEmitter<L.LeafletMouseEvent>();

    private map: LeafletMap | null = null;
    private markers: Marker[] = [];
    private routeControls: L.Routing.Control[] = [];
    private routeLines: Polyline[] = [];

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
            maxBounds: TUNISIA_BOUNDS,
            maxBoundsViscosity: 1.0,
        });

        // Add OpenStreetMap tile layer
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '© OpenStreetMap contributors',
            maxZoom: 19,
        }).addTo(this.map);

        // Register map click event
        this.map.on('click', (event: L.LeafletMouseEvent) => {
            this.mapClick.emit(event);
        });

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
        this.routeControls.forEach((control) => control.remove());
        this.routeControls = [];
        this.routeLines.forEach((line) => line.remove());
        this.routeLines = [];

        // Filter locations with valid coordinates
        const validLocations = this.locations.filter(
            loc => loc.latitude != null && loc.longitude != null
        );

        if (validLocations.length === 0) {
            return;
        }

        // Add markers for each location
        validLocations.forEach(location => {
            const icon = this.createMarkerIcon(location);
            
            const marker = L.marker([location.latitude, location.longitude], { icon })
                .addTo(this.map!)
                .bindPopup(this.createPopupContent(location));

            this.markers.push(marker);
        });

        this.drawCircuitRoutes(validLocations);

        // Fit map bounds to show all markers
        if (validLocations.length > 0) {
            const bounds = L.latLngBounds(
                validLocations.map(loc => [loc.latitude, loc.longitude] as [number, number])
            );
            this.map.fitBounds(bounds, { padding: [50, 50], maxZoom: 12 });
        }
    }

    private drawCircuitRoutes(validLocations: MapLocation[]): void {
        if (!this.map) {
            return;
        }

        const locationsByCircuit = new Map<string, MapLocation[]>();
        validLocations.forEach((location) => {
            if (!location.circuitId) {
                return;
            }

            const circuitLocations = locationsByCircuit.get(location.circuitId) ?? [];
            circuitLocations.push(location);
            locationsByCircuit.set(location.circuitId, circuitLocations);
        });

        locationsByCircuit.forEach((locations) => {
            const departure = locations.find((location) => location.pointType === 'departure');
            const arrival = locations.find((location) => location.pointType === 'arrival');
            const circuitColor = locations[0]?.color || '#2563eb';

            if (!departure || !arrival) {
                return;
            }

            const waypoints = [
                L.latLng(departure.latitude, departure.longitude),
                L.latLng(arrival.latitude, arrival.longitude),
            ];

            const routingControl = L.Routing.control({
                waypoints,
                show: false,
                addWaypoints: false,
                fitSelectedRoutes: false,
                routeWhileDragging: false,
                createMarker: () => null,
                lineOptions: {
                    styles: [{ color: circuitColor, weight: 3, opacity: 0.8 }],
                    extendToWaypoints: true,
                    missingRouteTolerance: 0,
                },
            } as any)
                .on('routingerror', () => {
                    const fallbackLine = L.polyline(
                        [
                            [departure.latitude, departure.longitude],
                            [arrival.latitude, arrival.longitude],
                        ],
                        {
                            color: circuitColor,
                            weight: 3,
                            opacity: 0.8,
                        }
                    ).addTo(this.map!);

                    this.routeLines.push(fallbackLine);
                })
                .addTo(this.map);

            this.routeControls.push(routingControl);
        });
    }

    private createMarkerIcon(location: MapLocation): L.Icon {
        let iconUrl = 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png';
        if (location.pointType === 'arrival') {
            iconUrl = 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png';
        } else if (location.pointType === 'base') {
            iconUrl = location.isActive === false
                ? 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-grey.png'
                : 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-blue.png';
        }

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
        this.routeControls.forEach((control) => control.remove());
        this.routeControls = [];

        this.routeLines.forEach((line) => line.remove());
        this.routeLines = [];

        if (this.map) {
            this.map.remove();
            this.map = null;
        }
    }
}
