export interface Bus {
    busId: string;
    numeroIMM: string;
    modelBus?: string;
    imei?: string;
    capacite?: number;
    codeCircuit?: string;
    appSagem: boolean;
    isActive: boolean;
    latitude?: number;
    longitude?: number;
    societeId: string;
}

export interface PagedBus {
    buses: Bus[];
    totalCount: number;
}
