import { Injectable } from '@angular/core';
import {
    Bus,
    BusLivePositionSnapshot,
    BusRuntimeState,
} from 'app/core/bus/bus.model';
import { MapLocation } from 'app/shared/components/map-viewer/map-viewer.component';

export interface BusTrackingItem {
    busId: string;
    numeroIMM: string;
    imei?: string;
    societeId: string;
    isActive: boolean;
    capacite?: number;
    latitude?: number;
    longitude?: number;
    currentOccupancy?: number;
    lastPositionAt?: string;
    lastOccupancyUpdateAt?: string;
}

@Injectable({ providedIn: 'root' })
export class BusTrackingAdapterService {
    buildTrackingItems(
        buses: Bus[],
        snapshot?: BusLivePositionSnapshot | null
    ): BusTrackingItem[] {
        const runtimeByBusId = new Map(
            (snapshot?.buses ?? []).map((b) => [b.busId, b] as const)
        );

        return (buses ?? []).map((bus) => {
            const runtime = runtimeByBusId.get(bus.busId);

            return {
                busId: bus.busId,
                numeroIMM: bus.numeroIMM,
                imei: runtime?.imei ?? bus.imei,
                societeId: bus.societeId,
                isActive: bus.isActive,
                capacite: bus.capacite ?? undefined,
                latitude: runtime?.latitude ?? bus.latitude,
                longitude: runtime?.longitude ?? bus.longitude,
                currentOccupancy:
                    runtime?.currentOccupancy ?? bus.currentOccupancy ?? undefined,
                lastPositionAt: runtime?.lastPositionAt ?? bus.lastPositionAt,
                lastOccupancyUpdateAt:
                    bus.lastOccupancyUpdateAt ?? runtime?.lastPositionAt,
            };
        });
    }

    mergeRuntimeState(
        items: BusTrackingItem[],
        runtime: BusRuntimeState
    ): BusTrackingItem[] {
        if (!runtime?.busId) {
            return items;
        }

        return (items ?? []).map((item) => {
            if (item.busId !== runtime.busId) {
                return item;
            }

            return {
                ...item,
                imei: runtime.imei ?? item.imei,
                latitude: runtime.latitude ?? item.latitude,
                longitude: runtime.longitude ?? item.longitude,
                currentOccupancy: runtime.currentOccupancy,
                lastPositionAt: runtime.lastPositionAt ?? item.lastPositionAt,
                lastOccupancyUpdateAt:
                    runtime.lastOccupancyUpdateAt ?? item.lastOccupancyUpdateAt,
            };
        });
    }

    getOccupancyRatio(item: BusTrackingItem | null | undefined): number | null {
        const capacity = item?.capacite;
        const occupancy = item?.currentOccupancy;

        if (!capacity || capacity <= 0 || occupancy === null || occupancy === undefined) {
            return null;
        }

        return Math.max(0, Math.min(1, occupancy / capacity));
    }

    buildMapLocations(
        items: BusTrackingItem[],
        options: { selectedBusId?: string | null; showAll?: boolean } = {}
    ): MapLocation[] {
        const showAll = options.showAll ?? false;
        const selectedBusId = options.selectedBusId ?? null;

        const filtered = showAll
            ? items
            : selectedBusId
              ? items.filter((b) => b.busId === selectedBusId)
              : [];

        return (filtered ?? [])
            .filter((b) => b.latitude != null && b.longitude != null)
            .map((b) => ({
                id: b.busId,
                name: b.numeroIMM,
                latitude: b.latitude!,
                longitude: b.longitude!,
                isActive: b.isActive,
                pointType: 'base',
                description: this.formatPopupDescription(b),
            }));
    }

    private formatPopupDescription(item: BusTrackingItem): string {
        const occupancy = item.currentOccupancy ?? null;
        const capacity = item.capacite ?? null;

        if (occupancy === null || capacity === null) {
            return 'Occupancy: —';
        }

        return `Occupancy: ${occupancy}/${capacity}`;
    }
}

