import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { CircuitPointCollecte } from '../../../../../core/circuit/circuit-point-collecte.model';
import { CircuitPointCollecteService } from '../../../../../core/circuit/circuit-point-collecte.service';
import { Circuit } from '../../../../../core/circuit/circuit.model';

export interface CircuitPointWithType {
  circuitPointCollecteId: string;
  circuitId: string;
  codePointCollecte: string;
  libellePointCollecte?: string;
  latitude?: number;
  longitude?: number;
  ordre?: number;
  isActive?: boolean;
  pointCategory: 'departure' | 'collection' | 'arrival';
  name?: string;
}

export interface CircuitData {
  circuitId: string;
  circuitCode: string;
  circuitName: string;
  circuitColor: string;
  departure: CircuitPointWithType | null;
  arrival: CircuitPointWithType | null;
  collectionPoints: CircuitPointWithType[];
  allPoints: CircuitPointWithType[];
  coordinates: [number, number][];
}

@Injectable({ providedIn: 'root' })
export class BusTrackingCircuitService {
  constructor(private pointService: CircuitPointCollecteService) {}

  loadCircuitData(circuitId: string): Observable<CircuitData> {
    return this.pointService.getByCircuit(circuitId).pipe(
      map((points) => this.categorize(points, circuitId))
    );
  }

  categorize(points: CircuitPointCollecte[], circuitId: string): CircuitData {
    const valid = points
      .filter((p) => p.latitude != null && p.longitude != null)
      .sort((a, b) => (a.ordre ?? 0) - (b.ordre ?? 0));

    if (valid.length === 0) {
      return this.emptyCircuit(circuitId);
    }

    const departure: CircuitPointWithType = { ...valid[0], pointCategory: 'departure' };
    const arrival: CircuitPointWithType = {
      ...valid[valid.length - 1],
      pointCategory: 'arrival',
    };
    const collectionPoints: CircuitPointWithType[] = valid
      .slice(1, -1)
      .map((p) => ({ ...p, pointCategory: 'collection' }));

    return {
      circuitId,
      circuitCode: circuitId,
      circuitName: circuitId,
      circuitColor: '#2563eb',
      departure,
      arrival,
      collectionPoints,
      allPoints: [departure, ...collectionPoints, arrival],
      coordinates: valid.map((p) => [p.latitude!, p.longitude!] as [number, number]),
    };
  }

  emptyCircuit(circuitId: string): CircuitData {
    return {
      circuitId,
      circuitCode: circuitId,
      circuitName: circuitId,
      circuitColor: '#2563eb',
      departure: null,
      arrival: null,
      collectionPoints: [],
      allPoints: [],
      coordinates: [],
    };
  }
}
