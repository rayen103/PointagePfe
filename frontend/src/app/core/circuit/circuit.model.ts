export interface Circuit {
    circuitId: string;
    codeCircuit: string;
    libelleCircuit?: string;
    description?: string;
    latitude?: number;
    longitude?: number;
    isActive: boolean;
    societeId: string;
}

export interface PagedCircuit {
    circuits: Circuit[];
    totalCount: number;
}
