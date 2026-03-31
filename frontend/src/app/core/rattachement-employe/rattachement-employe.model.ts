export interface RattachementEmploye {
    rattachementEmployeId: string;
    rattachementId: string;
    matricule: string;
    nomPrenom?: string;
    dateDebut?: string;
    heureDebut?: string;
    dateFin?: string;
    heureFin?: string;
    nombreHeure?: number;
    cout?: number;
    coutGlobal?: number;
    typeRattachement?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedRattachementEmploye {
    rattachementEmployes: RattachementEmploye[];
    totalCount: number;
}
