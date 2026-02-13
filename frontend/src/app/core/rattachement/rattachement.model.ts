export interface Rattachement {
    rattachementId: string;
    numeroRattachement: string;
    exercice?: string;
    dateRattachement: Date;
    numeroChantier?: string;
    codeClient?: string;
    isInternal: boolean;
    cout?: number;
    type?: string;
    nature?: string;
    responsable?: string;
    heureDebut?: string;
    heureFin?: string;
    emplacement?: string;
    reference?: string;
    status?: string;
    dateCloture?: Date;
    remarque?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedRattachement {
    rattachements: Rattachement[];
    totalCount: number;
}
