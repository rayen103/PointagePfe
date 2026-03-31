export interface Shift {
    shiftId: string;
    codeShift: string;
    libelleShift?: string;
    jourSemaine?: string;
    heureDebut?: string;
    heureFin?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedShift {
    shifts: Shift[];
    totalCount: number;
}
