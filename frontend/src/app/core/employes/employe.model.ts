export interface Employe {
    employeId: string;
    matricule: string;
    rfid?: string;
    nom: string;
    prenom: string;
    codeCircuit?: string;
    codePointCollecte?: string;
    codeShift?: string;
    adresse?: string;
    codeGouvernorat?: string;
    codeRegion?: string;
    societeId: string;
}

export interface PagedEmploye {
    employes: Employe[];
    total: number;
}
