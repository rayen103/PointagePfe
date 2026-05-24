export interface Region {
    regionId: string;
    codeRegion: string;
    libelleRegion: string;
    codeGouvernorat: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedRegion {
    regions: Region[];
    totalCount: number;
}
