export interface Intervention {
    interventionId: string;
    numeroIntervention: string;
    description?: string;
    dateIntervention: string;
    typeIntervention?: string;
    statut?: string;
    cout?: number;
}

export interface PagedIntervention {
    interventions: Intervention[];
    total: number;
}
