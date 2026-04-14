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
import { LatLng, Map, Marker, Polyline } from 'leaflet';
import 'leaflet-routing-machine';

export interface MapRoutePoint {
    latitude: number;
    longitude: number;
    label?: string;
}

const TUNISIA_BOUNDS = L.latLngBounds(
    [30.1, 7.5],
    [37.6, 11.8]
);

@Component({
    selector: 'app-map-picker',
    standalone: true,
    templateUrl: './map-picker.component.html',
    styleUrl: './map-picker.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MapPickerComponent implements AfterViewInit, OnChanges, OnDestroy {
    private static nextMapId = 0;
    @Input() latitude: number | null = 36.8065; // Default to Tunis, Tunisia
    @Input() longitude: number | null = 10.1815;
    @Input() zoom: number = 7; // Show Tunisia
    @Input() height: string = '400px';
    @Input() routePoints: MapRoutePoint[] = [];
    @Output() locationChange = new EventEmitter<{ latitude: number; longitude: number }>();

    readonly mapElementId: string = `map-picker-${MapPickerComponent.nextMapId++}`;
    private map: Map | null = null;
    private marker: Marker | null = null;
    private routeMarkers: Marker[] = [];
    private routeControl: L.Routing.Control | null = null;
    private routePolyline: Polyline | null = null;

    ngAfterViewInit(): void {
        this.initMap();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (!this.map) {
            return;
        }

        if (changes['latitude'] || changes['longitude']) {
            this.syncMainMarkerPosition();
        }

        if (changes['routePoints']) {
            this.updateRouteOverlay();
        }
    }

    private initMap(): void {
        // Create the map centered on Tunisia
        const center: LatLng = L.latLng(
            this.latitude ?? 36.8065,
            this.longitude ?? 10.1815
        );

        this.map = L.map(this.mapElementId, {
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

        const customIcon = this.createMainMarkerIcon();

        // Add a marker if coordinates are provided
        if (this.latitude != null && this.longitude != null) {
            this.marker = L.marker(center, { icon: customIcon, draggable: true })
                .addTo(this.map)
                .bindPopup('Bus Position');

            this.setupMarkerDragHandler(this.marker);
        }

        // Add click event to place/move marker
        this.map.on('click', (event: L.LeafletMouseEvent) => {
            const { lat, lng } = event.latlng;

            if (this.marker) {
                // Move existing marker
                this.marker.setLatLng(event.latlng);
            } else {
                // Create new marker
                this.marker = L.marker(event.latlng, { icon: customIcon, draggable: true })
                    .addTo(this.map!)
                    .bindPopup('Bus Position');

                this.setupMarkerDragHandler(this.marker);
            }

            // Emit the location change
            this.locationChange.emit({
                latitude: lat,
                longitude: lng,
            });
        });

        this.updateRouteOverlay();
    }

    private syncMainMarkerPosition(): void {
        if (!this.map || this.latitude == null || this.longitude == null) {
            return;
        }

        const mainPosition = L.latLng(this.latitude, this.longitude);
        if (this.marker) {
            this.marker.setLatLng(mainPosition);
        } else {
            this.marker = L.marker(mainPosition, { icon: this.createMainMarkerIcon(), draggable: true })
                .addTo(this.map)
                .bindPopup('Bus Position');
            this.setupMarkerDragHandler(this.marker);
        }
    }

    private createMainMarkerIcon(): L.Icon {
        return L.icon({
            iconUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png',
            iconRetinaUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon-2x.png',
            shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            shadowSize: [41, 41],
        });
    }

    private updateRouteOverlay(): void {
        if (!this.map) {
            return;
        }

        this.routeMarkers.forEach((marker) => marker.remove());
        this.routeMarkers = [];

        if (this.routePolyline) {
            this.routePolyline.remove();
            this.routePolyline = null;
        }
        if (this.routeControl) {
            this.routeControl.remove();
            this.routeControl = null;
        }

        const validRoutePoints = (this.routePoints ?? [])
            .filter((point) => point.latitude != null && point.longitude != null);

        if (validRoutePoints.length === 0) {
            return;
        }

        const latLngs = validRoutePoints.map((point) => L.latLng(point.latitude, point.longitude));

        if (latLngs.length > 1) {
            this.routeControl = L.Routing.control({
                waypoints: latLngs,
                show: false,
                addWaypoints: false,
                draggableWaypoints: false,
                fitSelectedRoutes: false,
                routeWhileDragging: false,
                createMarker: () => null,
                lineOptions: {
                    styles: [{ color: '#2563eb', weight: 4, opacity: 0.8 }],
                },
            })
                .on('routingerror', () => {
                    this.routePolyline = L.polyline(latLngs, {
                        color: '#2563eb',
                        weight: 4,
                        opacity: 0.8,
                    }).addTo(this.map!);
                })
                .addTo(this.map);
        }

        validRoutePoints.forEach((point, index) => {
            const routeIcon = this.createRouteMarkerIcon(index, validRoutePoints.length);
            const markerLabel = point.label ?? (index === 0
                ? 'Departure'
                : index === validRoutePoints.length - 1
                    ? 'Arrival'
                    : `Point ${index + 1}`);

            const routeMarker = L.marker([point.latitude, point.longitude], { icon: routeIcon })
                .addTo(this.map!)
                .bindPopup(markerLabel);

            this.routeMarkers.push(routeMarker);
        });

        const boundsPoints = [...latLngs];
        if (this.marker) {
            boundsPoints.push(this.marker.getLatLng());
        }

        if (boundsPoints.length > 0) {
            this.map.fitBounds(L.latLngBounds(boundsPoints), { padding: [40, 40], maxZoom: 14 });
        }
    }

    private createRouteMarkerIcon(index: number, total: number): L.Icon {
        let iconUrl = 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-blue.png';
        if (index === 0) {
            iconUrl = 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png';
        } else if (index === total - 1) {
            iconUrl = 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png';
        }

        return L.icon({
            iconUrl,
            shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            shadowSize: [41, 41],
        });
    }

    private setupMarkerDragHandler(marker: Marker): void {
        marker.on('dragend', (event) => {
            const position = event.target.getLatLng();
            this.locationChange.emit({
                latitude: position.lat,
                longitude: position.lng,
            });
        });
    }

    ngOnDestroy(): void {
        this.routeMarkers.forEach((marker) => marker.remove());
        this.routeMarkers = [];

        if (this.routePolyline) {
            this.routePolyline.remove();
            this.routePolyline = null;
        }
        if (this.routeControl) {
            this.routeControl.remove();
            this.routeControl = null;
        }

        if (this.map) {
            this.map.remove();
            this.map = null;
        }
    }
}
