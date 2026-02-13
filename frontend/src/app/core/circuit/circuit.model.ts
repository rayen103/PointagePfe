export interface Circuit {
    circuitId: string;
    codeCircuit: string;
    libelleCircuit?: string;
    description?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedCircuit {
    circuits: Circuit[];
    totalCount: number;
}
