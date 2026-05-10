export interface OrdreTravail {
    ordreTravailId: string;
    numeroOrdreTravail: string;
    numeroChantier?: string;
    codeClient?: string;
    numeroBonCommande?: string;
    codeEquipe?: string;
    etatOT?: string;
    montant?: number;
    dateCreation?: Date;
    numeroConvention?: string;
    codeVehicule?: string;
    libelle?: string;
    isActive: boolean;
    societeId: string;
    predictedDurationHours?: number;
    predictionConfidence?: number;
    predictionSource?: string;
}

export interface PagedOrdreTravail {
    ordresTravail: OrdreTravail[];
    totalCount: number;
}
