export interface Equipe {
    equipeId: string;
    codeEquipe: string;
    libelleEquipe?: string;
    codeClient?: string;
    codeEntrepot?: string;
    codeTarif?: string;
    codeFournisseur?: string;
    responsable?: string;
    isInternal: boolean;
    codeVehicule?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedEquipe {
    equipes: Equipe[];
    totalCount: number;
}
