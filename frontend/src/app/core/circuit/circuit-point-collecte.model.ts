export interface CircuitPointCollecte {
    circuitPointCollecteId: string;
    circuitId: string;
    codePointCollecte: string;
    libellePointCollecte?: string;
    latitude?: number;
    longitude?: number;
    ordre?: number;
    isActive?: boolean; // Optional property for UI display
}

export interface CircuitPointCollecteList {
    items: CircuitPointCollecte[]; // Changed from circuitPointsCollecte to items
}
