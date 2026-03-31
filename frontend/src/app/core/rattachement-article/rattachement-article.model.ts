export interface RattachementArticle {
    rattachementArticleId: string;
    rattachementId: string;
    codeArticle: string;
    libelle?: string;
    quantite?: number;
    prixRevient?: number;
    tauxTVA?: number;
    codeUnite?: string;
    codeEntrepot?: string;
    typeRattachement?: string;
    numeroBonLivraison?: string;
    dateBonLivraison?: string;
    isActive: boolean;
    societeId: string;
}

export interface PagedRattachementArticle {
    rattachementArticles: RattachementArticle[];
    totalCount: number;
}
