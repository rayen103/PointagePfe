export interface Chauffeur {
    chauffeurId: string;
    codeChauffeur: string;
    nom: string;
    prenom?: string;
    cin?: string;
    rfidChauffeur?: string;
    externe: boolean;
    isActive: boolean;
    societeId: string;
}

export interface PagedChauffeur {
    chauffeurs: Chauffeur[];
    totalCount: number;
}
