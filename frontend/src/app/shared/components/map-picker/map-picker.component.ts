import {
    AfterViewInit,
    ChangeDetectionStrategy,
    Component,
    EventEmitter,
    Input,
    OnDestroy,
    Output,
    ViewEncapsulation,
} from '@angular/core';
import * as L from 'leaflet';
import { LatLng, Map, Marker } from 'leaflet';

@Component({
    selector: 'app-map-picker',
    standalone: true,
    templateUrl: './map-picker.component.html',
    styleUrl: './map-picker.component.scss',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MapPickerComponent implements AfterViewInit, OnDestroy {
    @Input() latitude: number | null = 36.8065; // Default to Tunis, Tunisia
    @Input() longitude: number | null = 10.1815;
    @Input() zoom: number = 7; // Show Tunisia
    @Input() height: string = '400px';
    @Output() locationChange = new EventEmitter<{ latitude: number; longitude: number }>();

    private map: Map | null = null;
    private marker: Marker | null = null;

    ngAfterViewInit(): void {
        this.initMap();
    }

    private initMap(): void {
        // Create the map centered on Tunisia
        const center: LatLng = L.latLng(
            this.latitude ?? 36.8065,
            this.longitude ?? 10.1815
        );

        this.map = L.map('map-picker', {
            center: center,
            zoom: this.zoom,
        });

        // Add OpenStreetMap tile layer
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '© OpenStreetMap contributors',
            maxZoom: 19,
        }).addTo(this.map);

        // Create custom icon for the marker
        const customIcon = L.icon({
            iconUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png',
            iconRetinaUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon-2x.png',
            shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            shadowSize: [41, 41],
        });

        // Add a marker if coordinates are provided
        if (this.latitude != null && this.longitude != null) {
            this.marker = L.marker(center, { icon: customIcon, draggable: true })
                .addTo(this.map)
                .bindPopup('Circuit Location');

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
                    .bindPopup('Circuit Location');

                this.setupMarkerDragHandler(this.marker);
            }

            // Emit the location change
            this.locationChange.emit({
                latitude: lat,
                longitude: lng,
            });
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
        if (this.map) {
            this.map.remove();
            this.map = null;
        }
    }
}
