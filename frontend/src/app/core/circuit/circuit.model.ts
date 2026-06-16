import { CircuitPointCollecte } from "./circuit-point-collecte.model";

export interface Circuit {
    circuitId: string;
    codeCircuit: string;
    libelleCircuit?: string;
    description?: string;
    latitude?: number;
    longitude?: number;
    isActive: boolean;
    societeId: string;
    codePCDepart?: string;
    codePCArrivee?: string;
    distanceKm?: number;
    dureeMinutes?: number;
    couleur?: string;
    circuitPointsCollecte?: CircuitPointCollecte[];
}

export interface PagedCircuit {
    circuits: Circuit[];
    totalCount: number;
}
