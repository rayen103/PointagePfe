export interface Societe{
    societeId:string;
    logoPath?:string;
    logoData?:string;
    logoExtension?:string;
    nom?:string;
    initiales?:string;
    tva?:string;
    rc?:string;
    matriculeFiscal?:string;
    rne?:string;
    capital?:number;
    dateOverture:string;
    telephone1?:string;
    telephone2?:string;
    fax1?:string;
    fax2?:string;
    email?:string;
    adresse?:string;
    codePostal?:string;
    ville?:string;
    pays?:string;
    codeSociete?:string;
}

export interface Site{
    siteId?:string;
    societeId:string;
    code:string;
    site:string;
    siege:boolean;
    longitude?:number;
    latitude?:number;
    rayon?:number;
    timeMinute?:number;
    isActive:boolean;
}

export interface Reseau{
    reseauId?:string;
    societeId:string;
    ipAddress:string;
    port:number;
    gmtPlus?:number;
    latitude?:number;
    longitude?:number;
    rayon?:number;
    timeToleranceMinute?:number;
    isActive:boolean;
}

export interface PagedSociete{
    societes:Societe[];
    length:number;
}
