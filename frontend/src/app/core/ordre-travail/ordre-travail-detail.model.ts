export interface OrdreTravailDetail {
    ordreTravailDetailId: string;
    ordreTravailId: string;
    codeArticle: string;
    codeEntrepot?: string;
    codeUnite?: string;
    libelleArticle?: string;
    prixUnitaireHT?: number;
    quantite?: number;
    tauxTVA?: number;
    montant?: number;
}

export interface OrdreTravailDetailList {
    items: OrdreTravailDetail[];
}
