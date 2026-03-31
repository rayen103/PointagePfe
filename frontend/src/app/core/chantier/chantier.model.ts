export interface Chantier {
    chantierId: string;
    numeroChantier: string;
    libelleChantier?: string;
    codeClient?: string;
    adresse?: string;
    montantHT?: number;
    montantTTC?: number;
    nature?: string;
    responsable?: string;
    dateDebut?: string;
    dateFin?: string;
    status?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedChantier {
    chantiers: Chantier[];
    totalCount: number;
}
