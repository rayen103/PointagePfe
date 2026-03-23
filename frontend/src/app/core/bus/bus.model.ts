export interface Bus {
    busId: string;
    numeroIMM: string;
    modelBus?: string;
    imei?: string;
    capacite?: number;
    codeCircuit?: string;
    appSagem: boolean;
    isActive: boolean;
    societeId: string;
}

export interface PagedBus {
    buses: Bus[];
    totalCount: number;
}
