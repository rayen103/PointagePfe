export interface Pointage {
    pointageId: string;
    tag: string;
    busId: string;
    busNumeroIMM?: string;
    imei?: string;
    matricule?: string;
    nomEmploye?: string;
    codeCircuitEmploye?: string;
    codeCircuitBus?: string;
    latitude?: number;
    longitude?: number;
    heurePointageUtc: string;
    receivedAtUtc: string;
    isSuccess: boolean;
    message: string;
    societeId: string;
}

export interface PagedPointage {
    pointages: Pointage[];
    totalCount: number;
}
