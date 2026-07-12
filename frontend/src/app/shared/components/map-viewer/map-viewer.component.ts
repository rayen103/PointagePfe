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
import { Map as LeafletMap, Marker, Polyline, Polygon, DivIcon } from 'leaflet';
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

export interface CircuitPointLocation extends MapLocation {
    pointCategory: 'departure' | 'collection' | 'arrival';
    ordre?: number;
}

export interface OptimizedRouteData {
    geometry: [number, number][];
    distanceKm?: number;
    durationMinutes?: number;
    orderedPointIds?: string[];
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
    @Input() circuitPoints: CircuitPointLocation[] = [];
    @Input() optimizedRoute: OptimizedRouteData | null = null;
    @Input() selectedBusPosition: { latitude: number; longitude: number; heading?: number } | null = null;
    @Input() circuitArea: [number, number][] | null = null;
    @Output() readonly mapClick = new EventEmitter<L.LeafletMouseEvent>();

    private map: LeafletMap | null = null;
    private markers: Marker[] = [];
    private routeControls: L.Routing.Control[] = [];
    private routeLines: Polyline[] = [];
    private circuitMarkers: Marker[] = [];
    private busMarker: Marker | null = null;
    private areaPolygon: Polygon | null = null;

    ngAfterViewInit(): void {
        this.initMap();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['locations'] && !changes['locations'].firstChange && this.map) {
            this.updateMarkers();
        }
        if (
            (changes['circuitPoints'] || changes['optimizedRoute'] || changes['selectedBusPosition'] || changes['circuitArea']) &&
            this.map
        ) {
            this.updateCircuitOverlays();
        }
    }

    private initMap(): void {
        const center = L.latLng(36.8065, 10.1815);

        this.map = L.map('map-viewer', {
            center: center,
            zoom: this.zoom,
            maxBounds: TUNISIA_BOUNDS,
            maxBoundsViscosity: 1.0,
        });

        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '© OpenStreetMap contributors',
            maxZoom: 19,
        }).addTo(this.map);

        this.map.on('click', (event: L.LeafletMouseEvent) => {
            this.mapClick.emit(event);
        });

        this.updateMarkers();
        this.updateCircuitOverlays();

        setTimeout(() => {
            if (this.map) {
                this.map.invalidateSize();
            }
        }, 200);
    }

    private updateMarkers(): void {
        if (!this.map) {
            return;
        }

        this.markers.forEach((marker) => marker.remove());
        this.markers = [];
        this.routeControls.forEach((control) => control.remove());
        this.routeControls = [];
        this.routeLines.forEach((line) => line.remove());
        this.routeLines = [];

        const validLocations = this.locations.filter(
            (loc) => loc.latitude != null && loc.longitude != null
        );

        if (validLocations.length === 0) {
            return;
        }

        validLocations.forEach((location) => {
            const icon = this.createMarkerIcon(location);

            const marker = L.marker([location.latitude, location.longitude], { icon })
                .addTo(this.map!)
                .bindPopup(this.createPopupContent(location));

            this.markers.push(marker);
        });

        this.drawCircuitRoutes(validLocations);

        if (validLocations.length > 0) {
            const bounds = L.latLngBounds(
                validLocations.map((loc) => [loc.latitude, loc.longitude] as [number, number])
            );
            this.map.fitBounds(bounds, { padding: [50, 50], maxZoom: 12 });
        }
    }

    private updateCircuitOverlays(): void {
        if (!this.map) {
            return;
        }

        this.circuitMarkers.forEach((marker) => marker.remove());
        this.circuitMarkers = [];

        if (this.busMarker) {
            this.busMarker.remove();
            this.busMarker = null;
        }

        this.routeLines.forEach((line) => line.remove());
        this.routeLines = [];

        if (this.areaPolygon) {
            this.areaPolygon.remove();
            this.areaPolygon = null;
        }

        if (this.selectedBusPosition) {
            this.busMarker = this.createBusMarker(
                this.selectedBusPosition.latitude,
                this.selectedBusPosition.longitude,
                this.selectedBusPosition.heading
            ).addTo(this.map);
        }

        if (this.circuitArea && this.circuitArea.length >= 3) {
            this.areaPolygon = L.polygon(this.circuitArea, {
                color: '#2563eb',
                weight: 1,
                opacity: 0.25,
                fillColor: '#42abe0',
                fillOpacity: 0.08,
                dashArray: '6 4',
            }).addTo(this.map);
        }

        if (this.optimizedRoute?.geometry && this.optimizedRoute.geometry.length > 1) {
            const routeLine = L.polyline(this.optimizedRoute.geometry, {
                color: '#2563eb',
                weight: 4,
                opacity: 0.85,
                smoothFactor: 1,
            }).addTo(this.map);
            this.routeLines.push(routeLine);
        }

        (this.circuitPoints ?? []).forEach((point, index) => {
            if (point.latitude == null || point.longitude == null) {
                return;
            }

            const marker = this.createCircuitPointMarker(point, index).addTo(this.map);
            this.circuitMarkers.push(marker);
        });
    }

    private createCircuitPointMarker(point: CircuitPointLocation, index: number): Marker {
        const isArrival = point.pointCategory === 'arrival';
        const isDeparture = point.pointCategory === 'departure';
        const isCollection = point.pointCategory === 'collection';

        let iconUrl: string;
        if (isArrival) {
            iconUrl = 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png';
        } else if (isDeparture) {
            iconUrl = 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png';
        } else {
            iconUrl = 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-orange.png';
        }

        const icon = L.icon({
            iconUrl: iconUrl,
            shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            shadowSize: [41, 41],
        });

        const labelContent = isCollection
            ? `<div style="
                  background:#f97316;
                  color:#fff;
                  width:20px;
                  height:20px;
                  border-radius:50%;
                  display:flex;
                  align-items:center;
                  justify-content:center;
                  font-size:11px;
                  font-weight:700;
                  border:2px solid #fff;
                  box-shadow:0 1px 4px rgba(0,0,0,0.3);
                ">${index}</div>`
            : '';

        const marker = L.marker([point.latitude, point.longitude], { icon });

        if (labelContent) {
            const labelIcon = L.divIcon({
                html: labelContent,
                className: 'circuit-point-label',
                iconSize: [20, 20],
                iconAnchor: [10, 10],
            });

            L.marker([point.latitude, point.longitude], { icon: labelIcon, interactive: false }).addTo(this.map!);
        }

        marker.bindPopup(this.createCircuitPopupContent(point, index));
        return marker;
    }

    private createBusMarker(lat: number, lng: number, heading?: number): Marker {
        const rotation = heading != null ? heading : 0;

        const pulseIcon = L.divIcon({
            html: `
                <div style="position:relative; width:36px; height:36px; transform: translate(-50%, -50%);">
                    <div style="
                        position:absolute;
                        inset:0;
                        border-radius:50%;
                        background:rgba(66,171,224,0.25);
                        animation: bus-pulse 2s ease-out infinite;
                    "></div>
                    <div style="
                        position:absolute;
                        inset:6px;
                        border-radius:50%;
                        background:#2563eb;
                        border:2px solid #fff;
                        box-shadow:0 2px 8px rgba(37,99,235,0.45);
                        display:flex;
                        align-items:center;
                        justify-content:center;
                    ">
                        <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="#fff" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" style="transform: rotate(${rotation}deg);">
                            <path d="M5 17h14M5 17l1-7h12l1 7M6 10h.01M18 10h.01"/>
                        </svg>
                    </div>
                </div>
                <style>
                    @keyframes bus-pulse {
                        0% { transform: scale(1); opacity: 0.7; }
                        100% { transform: scale(2.4); opacity: 0; }
                    }
                </style>
            `,
            className: 'bus-marker-container',
            iconSize: [36, 36],
            iconAnchor: [18, 18],
        });

        return L.marker([lat, lng], { icon: pulseIcon, zIndexOffset: 1000 });
    }

    private createCircuitPopupContent(point: CircuitPointLocation, index: number): string {
        const categoryLabel =
            point.pointCategory === 'departure'
                ? 'Départ'
                : point.pointCategory === 'arrival'
                  ? 'Arrivée'
                  : `Point ${index}`;

        const ordre = point.ordre != null ? `Ordre: ${point.ordre}` : '';

        return `
            <div class="p-2">
                <div class="font-bold text-base mb-1">${point.name}</div>
                <div class="text-xs font-semibold uppercase tracking-wide mb-1" style="color: ${point.pointCategory === 'departure' ? '#0E8A5F' : point.pointCategory === 'arrival' ? '#D12B28' : '#F97316'}">${categoryLabel}</div>
                ${ordre ? `<div class="text-xs text-gray-500">${ordre}</div>` : ''}
                <div class="text-xs text-gray-500 mt-2">
                    ${point.latitude.toFixed(6)}, ${point.longitude.toFixed(6)}
                </div>
            </div>
        `;
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
        this.circuitMarkers.forEach((marker) => marker.remove());
        this.circuitMarkers = [];

        if (this.busMarker) {
            this.busMarker.remove();
            this.busMarker = null;
        }

        if (this.areaPolygon) {
            this.areaPolygon.remove();
            this.areaPolygon = null;
        }

        this.routeControls.forEach((control) => control.remove());
        this.routeControls = [];

        this.routeLines.forEach((line) => line.remove());
        this.routeLines = [];

        this.markers.forEach((marker) => marker.remove());
        this.markers = [];

        if (this.map) {
            this.map.remove();
            this.map = null;
        }
    }
}
