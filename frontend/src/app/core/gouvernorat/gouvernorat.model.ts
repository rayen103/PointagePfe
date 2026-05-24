export interface Gouvernorat {
    gouvernoratId: string;
    codeGouvernorat: string;
    libelleGouvernorat: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedGouvernorat {
    gouvernorats: Gouvernorat[];
    totalCount: number;
}
