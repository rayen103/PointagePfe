export type TypeEmploye = 'EmployeSimple' | 'Chauffeur';

export interface Employe {
    employeId: string;
    matricule: string;
    rfid?: string;
    nom: string;
    prenom: string;
    typeEmploye: TypeEmploye;
    codeCircuit?: string;
    codePointCollecte?: string;
    codeBus?: string;
    codeShift?: string;
    adresse?: string;
    codeGouvernorat?: string;
    codeRegion?: string;
    latitude?: number;
    longitude?: number;
    societeId: string;
}

export interface PagedEmploye {
    employes: Employe[];
    total: number;
}
