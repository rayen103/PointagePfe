export interface Modem {
    modemId: string;
    imei: string;
    modelModem?: string;
    numeroSim?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedModem {
    modems: Modem[];
    totalCount: number;
}
