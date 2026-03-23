export interface CircuitPointCollecte {
    circuitPointCollecteId: string;
    circuitId: string;
    codePointCollecte: string;
    libellePointCollecte?: string;
    latitude?: number;
    longitude?: number;
    ordre?: number;
}

export interface CircuitPointCollecteList {
    circuitPointsCollecte: CircuitPointCollecte[];
}
