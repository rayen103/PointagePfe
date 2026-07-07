export interface Employe {
    employeId: string;
    matricule: string;
    rfid?: string;
    nom: string;
    prenom: string;
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
    isActive: boolean;
    absenceRiskScore?: number;
    absenceRiskLevel?: 'low' | 'medium' | 'high';
    absencePredictionConfidence?: number;
}

export interface PagedEmploye {
    employes: Employe[];
    total: number;
}
