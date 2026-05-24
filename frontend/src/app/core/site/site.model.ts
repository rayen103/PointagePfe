export interface Site {
    siteId: string;
    code: string;
    libelleSite: string;
    siege: boolean;
    longitude?: number;
    latitude?: number;
    rayon?: number;
    timeMinute?: number;
    isActive: boolean;
    societeId: string;
}

export interface PagedSite {
    sites: Site[];
    totalCount: number;
}