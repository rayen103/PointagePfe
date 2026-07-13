export interface Bus {
    busId: string;
    numeroIMM: string;
    modelBus?: string;
    imei?: string;
    capacite?: number;
    codeCircuit?: string;
    codeChauffeur?: string;
    appSagem: boolean;
    isActive: boolean;
    latitude?: number;
    longitude?: number;
    currentOccupancy?: number;
    lastPositionAt?: string;
    lastOccupancyUpdateAt?: string;
    societeId: string;
}

export interface PagedBus {
    buses: Bus[];
    totalCount: number;
}

export interface BusRuntimeState {
    busId: string;
    numeroIMM: string;
    imei?: string;
    latitude?: number;
    longitude?: number;
    currentOccupancy: number;
    lastPositionAt?: string;
    lastOccupancyUpdateAt?: string;
}

export interface BusLivePosition {
    busId: string;
    numeroIMM: string;
    imei?: string;
    latitude?: number;
    longitude?: number;
    currentOccupancy: number;
    lastPositionAt?: string;
}

export interface BusLivePositionSnapshot {
    generatedAtUtc: string;
    buses: BusLivePosition[];
}

export interface BusRuntimeEvent {
    busRuntimeEventId: string;
    busId: string;
    eventType: string;
    description: string;
    imei?: string;
    latitude?: number;
    longitude?: number;
    occupancy?: number;
    occurredAtUtc: string;
}

export interface BusPointage {
    pointageId: string;
    busId: string;
    tag: string;
    matricule?: string;
    nomEmploye?: string;
    codeCircuitEmploye?: string;
    codeCircuitBus?: string;
    isSuccess: boolean;
    message: string;
    latitude?: number;
    longitude?: number;
    heurePointageUtc: string;
}

export interface UpdateBusRuntimePositionPayload {
    imei: string;
    latitude?: number;
    longitude?: number;
    occupancy?: number;
    timestampUtc?: string;
}
