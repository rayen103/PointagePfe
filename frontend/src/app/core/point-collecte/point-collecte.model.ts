export interface PointCollecte {
    pointCollecteId: string;
    codePointCollecte: string;
    libellePointCollecte?: string;
    latitude?: number;
    longitude?: number;
    codeGouvernorat?: string;
    codeRegion?: string;
    isActive: boolean;
    societeId: string;
    circuitId?: string;
}

export interface PagedPointCollecte {
    pointsCollecte: PointCollecte[];
    totalCount: number;
}
