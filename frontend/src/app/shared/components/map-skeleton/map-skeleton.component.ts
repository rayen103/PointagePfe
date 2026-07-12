import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

/**
 * Lightweight placeholder shown while a Leaflet map is being lazy-loaded.
 * Pure CSS (shimmer + fake roads/pin) — costs nothing to render.
 */
@Component({
    selector: 'app-map-skeleton',
    standalone: true,
    template: `
        <div class="map-skel" [style.height]="height" role="status" aria-label="Chargement de la carte">
            <div class="map-skel__roads">
                <span class="map-skel__road map-skel__road--1"></span>
                <span class="map-skel__road map-skel__road--2"></span>
                <span class="map-skel__road map-skel__road--3"></span>
                <span class="map-skel__road map-skel__road--v1"></span>
                <span class="map-skel__road map-skel__road--v2"></span>
            </div>
            <div class="map-skel__pin">
                <svg viewBox="0 0 24 24" width="26" height="26" fill="currentColor">
                    <path d="M12 2C8.13 2 5 5.13 5 9c0 5.25 7 13 7 13s7-7.75 7-13c0-3.87-3.13-7-7-7zm0 9.5A2.5 2.5 0 1 1 12 6.5a2.5 2.5 0 0 1 0 5z"/>
                </svg>
                <span class="map-skel__label">Chargement de la carte…</span>
            </div>
            <div class="map-skel__shimmer"></div>
        </div>
    `,
    styles: `
        .map-skel {
            position: relative; width: 100%; height: 100%; min-height: 220px;
            background: #EAF0F6; border-radius: inherit; overflow: hidden;
        }
        .map-skel__roads { position: absolute; inset: 0; opacity: 0.6; }
        .map-skel__road { position: absolute; background: #D9E4EF; border-radius: 4px; }
        .map-skel__road--1 { top: 22%; left: -5%; width: 70%; height: 8px; transform: rotate(-4deg); }
        .map-skel__road--2 { top: 48%; left: 15%; width: 90%; height: 10px; transform: rotate(3deg); }
        .map-skel__road--3 { top: 74%; left: -8%; width: 65%; height: 7px; transform: rotate(-2deg); }
        .map-skel__road--v1 { top: -5%; left: 30%; width: 8px; height: 75%; transform: rotate(8deg); }
        .map-skel__road--v2 { top: 25%; left: 68%; width: 7px; height: 85%; transform: rotate(-6deg); }
        .map-skel__pin {
            position: absolute; inset: 0; display: flex; flex-direction: column;
            align-items: center; justify-content: center; gap: 8px; color: #8CA2BD;
        }
        .map-skel__pin svg { animation: map-skel-bounce 1.4s ease-in-out infinite; }
        .map-skel__label { font-size: 12px; font-weight: 600; color: #8CA2BD; }
        .map-skel__shimmer {
            position: absolute; inset: 0;
            background: linear-gradient(105deg, transparent 40%, rgba(255, 255, 255, 0.55) 50%, transparent 60%);
            background-size: 220% 100%;
            animation: map-skel-shimmer 1.6s linear infinite;
        }
        @keyframes map-skel-shimmer {
            from { background-position: 130% 0; }
            to { background-position: -90% 0; }
        }
        @keyframes map-skel-bounce {
            0%, 100% { transform: translateY(0); }
            50% { transform: translateY(-6px); }
        }
    `,
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MapSkeletonComponent {
    @Input() height: string = '100%';
}
