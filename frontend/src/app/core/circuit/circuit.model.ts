export interface Circuit {
    circuitId: string;
    codeCircuit: string;
    libelleCircuit?: string;
    description?: string;
    latitude?: number;
    longitude?: number;
    isActive: boolean;
    societeId: string;
    codePCDepart?: string;
    codePCArrivee?: string;
    distanceKm?: number;
    dureeMinutes?: number;
    couleur?: string;
}

export interface PagedCircuit {
    circuits: Circuit[];
    totalCount: number;
}
