import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, of, tap } from 'rxjs';
import { Pointage, PagedPointage } from './pointage.model';
import { ApiService } from '../common/api.service';

const buildMockPointages = (): Pointage[] => {
    const now = Date.now();
    return [
        {
            pointageId: '01HV8X1001MOCKPTG000000001',
            tag: 'RFID-E1042',
            busId: 'BUS-001',
            busNumeroIMM: '184 TUNIS 5241',
            imei: '864201045938201',
            matricule: 'EMP-0412',
            nomEmploye: 'Mohamed Trabelsi',
            codeCircuitEmploye: 'CIRCUIT-NORD',
            codeCircuitBus: 'CIRCUIT-NORD',
            latitude: 36.8065,
            longitude: 10.1815,
            heurePointageUtc: new Date(now - 8 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 8 * 60 * 1000).toISOString(),
            isSuccess: true,
            message: 'Pointage validé avec succès à bord.',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000002',
            tag: 'RFID-E1088',
            busId: 'BUS-001',
            busNumeroIMM: '184 TUNIS 5241',
            imei: '864201045938201',
            matricule: 'EMP-0883',
            nomEmploye: 'Sami Ben Ali',
            codeCircuitEmploye: 'CIRCUIT-NORD',
            codeCircuitBus: 'CIRCUIT-NORD',
            latitude: 36.8090,
            longitude: 10.1850,
            heurePointageUtc: new Date(now - 14 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 14 * 60 * 1000).toISOString(),
            isSuccess: true,
            message: 'Pointage validé avec succès à bord.',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000003',
            tag: 'RFID-E1120',
            busId: 'BUS-002',
            busNumeroIMM: '210 TUNIS 9812',
            imei: '864201045938202',
            matricule: 'EMP-1120',
            nomEmploye: 'Amira Jaziri',
            codeCircuitEmploye: 'CIRCUIT-SUD',
            codeCircuitBus: 'CIRCUIT-SUD',
            latitude: 36.7540,
            longitude: 10.2210,
            heurePointageUtc: new Date(now - 22 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 22 * 60 * 1000).toISOString(),
            isSuccess: true,
            message: 'Pointage validé avec succès à bord.',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000004',
            tag: 'RFID-E1305',
            busId: 'BUS-003',
            busNumeroIMM: '145 TUNIS 3102',
            imei: '864201045938203',
            matricule: 'EMP-1305',
            nomEmploye: 'Youssef Gharbi',
            codeCircuitEmploye: 'CIRCUIT-EST',
            codeCircuitBus: 'CIRCUIT-OUEST',
            latitude: 36.7980,
            longitude: 10.1600,
            heurePointageUtc: new Date(now - 31 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 31 * 60 * 1000).toISOString(),
            isSuccess: false,
            message: 'Circuit non concordant (Attendu: CIRCUIT-EST, Bus: CIRCUIT-OUEST).',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000005',
            tag: 'RFID-E1410',
            busId: 'BUS-002',
            busNumeroIMM: '210 TUNIS 9812',
            imei: '864201045938202',
            matricule: 'EMP-1410',
            nomEmploye: 'Fatma Mansouri',
            codeCircuitEmploye: 'CIRCUIT-SUD',
            codeCircuitBus: 'CIRCUIT-SUD',
            latitude: 36.7580,
            longitude: 10.2280,
            heurePointageUtc: new Date(now - 42 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 42 * 60 * 1000).toISOString(),
            isSuccess: true,
            message: 'Pointage validé avec succès à bord.',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000006',
            tag: 'RFID-E1552',
            busId: 'BUS-001',
            busNumeroIMM: '184 TUNIS 5241',
            imei: '864201045938201',
            matricule: 'EMP-1552',
            nomEmploye: 'Karim Dridi',
            codeCircuitEmploye: 'CIRCUIT-NORD',
            codeCircuitBus: 'CIRCUIT-NORD',
            latitude: 36.8120,
            longitude: 10.1900,
            heurePointageUtc: new Date(now - 50 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 50 * 60 * 1000).toISOString(),
            isSuccess: true,
            message: 'Pointage validé avec succès à bord.',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000007',
            tag: 'RFID-UNKNOWN',
            busId: 'BUS-003',
            busNumeroIMM: '145 TUNIS 3102',
            imei: '864201045938203',
            matricule: '',
            nomEmploye: 'Inconnu',
            codeCircuitEmploye: '—',
            codeCircuitBus: 'CIRCUIT-OUEST',
            latitude: 36.8010,
            longitude: 10.1650,
            heurePointageUtc: new Date(now - 58 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 58 * 60 * 1000).toISOString(),
            isSuccess: false,
            message: 'Badge RFID non reconnu dans le référentiel des employés.',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000008',
            tag: 'RFID-E1620',
            busId: 'BUS-001',
            busNumeroIMM: '184 TUNIS 5241',
            imei: '864201045938201',
            matricule: 'EMP-1620',
            nomEmploye: 'Nour Bouazizi',
            codeCircuitEmploye: 'CIRCUIT-NORD',
            codeCircuitBus: 'CIRCUIT-NORD',
            latitude: 36.8150,
            longitude: 10.1940,
            heurePointageUtc: new Date(now - 66 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 66 * 60 * 1000).toISOString(),
            isSuccess: true,
            message: 'Pointage validé avec succès à bord.',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000009',
            tag: 'RFID-E1744',
            busId: 'BUS-002',
            busNumeroIMM: '210 TUNIS 9812',
            imei: '864201045938202',
            matricule: 'EMP-1744',
            nomEmploye: 'Zied Mahjoub',
            codeCircuitEmploye: 'CIRCUIT-SUD',
            codeCircuitBus: 'CIRCUIT-SUD',
            latitude: 36.7610,
            longitude: 10.2330,
            heurePointageUtc: new Date(now - 78 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 78 * 60 * 1000).toISOString(),
            isSuccess: true,
            message: 'Pointage validé avec succès à bord.',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000010',
            tag: 'RFID-E1802',
            busId: 'BUS-003',
            busNumeroIMM: '145 TUNIS 3102',
            imei: '864201045938203',
            matricule: 'EMP-1802',
            nomEmploye: 'Rim Khemir',
            codeCircuitEmploye: 'CIRCUIT-OUEST',
            codeCircuitBus: 'CIRCUIT-OUEST',
            latitude: 36.8040,
            longitude: 10.1700,
            heurePointageUtc: new Date(now - 90 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 90 * 60 * 1000).toISOString(),
            isSuccess: true,
            message: 'Pointage validé avec succès à bord.',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000011',
            tag: 'RFID-E1850',
            busId: 'BUS-004',
            busNumeroIMM: '198 TUNIS 6074',
            imei: '864201045938204',
            matricule: 'EMP-1850',
            nomEmploye: 'Ahmed Boukhris',
            codeCircuitEmploye: 'CIRCUIT-Z-IND',
            codeCircuitBus: 'CIRCUIT-Z-IND',
            latitude: 36.8320,
            longitude: 10.1450,
            heurePointageUtc: new Date(now - 105 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 105 * 60 * 1000).toISOString(),
            isSuccess: true,
            message: 'Pointage validé avec succès à bord.',
            societeId: 'SOC-001'
        },
        {
            pointageId: '01HV8X1001MOCKPTG000000012',
            tag: 'RFID-E1901',
            busId: 'BUS-004',
            busNumeroIMM: '198 TUNIS 6074',
            imei: '864201045938204',
            matricule: 'EMP-1901',
            nomEmploye: 'Marwa Rekik',
            codeCircuitEmploye: 'CIRCUIT-Z-IND',
            codeCircuitBus: 'CIRCUIT-Z-IND',
            latitude: 36.8350,
            longitude: 10.1480,
            heurePointageUtc: new Date(now - 118 * 60 * 1000).toISOString(),
            receivedAtUtc: new Date(now - 118 * 60 * 1000).toISOString(),
            isSuccess: true,
            message: 'Pointage validé avec succès à bord.',
            societeId: 'SOC-001'
        }
    ];
};

@Injectable({ providedIn: 'root' })
export class PointageService {
    private _pointages = new BehaviorSubject<Pointage[] | null>([]);
    private _pointagesLength = new BehaviorSubject<number | null>(0);
    private _mockPointages: Pointage[] = buildMockPointages();

    constructor(private _apiservice: ApiService) {}

    get pointages$(): Observable<Pointage[]> {
        return this._pointages.asObservable().pipe(map(list => list ?? []));
    }

    get pointagesLength$(): Observable<number> {
        return this._pointagesLength.asObservable().pipe(map(len => len ?? 0));
    }

    GetPointages(
        page = 1,
        size = 10,
        sort = 'heurePointageUtc',
        order: 'asc' | 'desc' | '' = 'desc',
        search = '',
        filters?: { busId?: string; isSuccess?: boolean; startDate?: string; endDate?: string }
    ): Observable<PagedPointage> {
        let params: any = {
            search: search || '',
            sort,
            order,
            page: page.toString(),
            size: size.toString()
        };

        if (filters?.busId) {
            params.busId = filters.busId;
        }
        if (filters?.isSuccess !== undefined && filters.isSuccess !== null) {
            params.isSuccess = filters.isSuccess.toString();
        }
        if (filters?.startDate) {
            params.startDate = filters.startDate;
        }
        if (filters?.endDate) {
            params.endDate = filters.endDate;
        }

        return this._apiservice.Get<PagedPointage>('pointage/list', { params }).pipe(
            catchError(() => of(null as any)),
            map(r => {
                if (r?.data?.pointages && r.data.pointages.length > 0) {
                    this._pointages.next(r.data.pointages);
                    this._pointagesLength.next(r.data.totalCount || r.data.pointages.length);
                    return r.data;
                }

                // Apply in-memory filtering on mock data
                let list = [...this._mockPointages];

                if (search && search.trim() !== '') {
                    const q = search.trim().toLowerCase();
                    list = list.filter(p =>
                        (p.nomEmploye && p.nomEmploye.toLowerCase().includes(q)) ||
                        (p.matricule && p.matricule.toLowerCase().includes(q)) ||
                        (p.tag && p.tag.toLowerCase().includes(q)) ||
                        (p.busNumeroIMM && p.busNumeroIMM.toLowerCase().includes(q)) ||
                        (p.codeCircuitEmploye && p.codeCircuitEmploye.toLowerCase().includes(q)) ||
                        (p.codeCircuitBus && p.codeCircuitBus.toLowerCase().includes(q)) ||
                        (p.message && p.message.toLowerCase().includes(q))
                    );
                }

                if (filters?.busId) {
                    const bId = filters.busId.toLowerCase();
                    list = list.filter(p =>
                        (p.busId && p.busId.toLowerCase() === bId) ||
                        (p.busNumeroIMM && p.busNumeroIMM.toLowerCase() === bId)
                    );
                }

                if (filters?.isSuccess !== undefined && filters?.isSuccess !== null) {
                    list = list.filter(p => p.isSuccess === filters.isSuccess);
                }

                if (filters?.startDate) {
                    const start = new Date(filters.startDate).getTime();
                    list = list.filter(p => new Date(p.heurePointageUtc).getTime() >= start);
                }

                if (filters?.endDate) {
                    const end = new Date(filters.endDate).getTime();
                    list = list.filter(p => new Date(p.heurePointageUtc).getTime() <= end);
                }

                // Sorting
                list.sort((a: any, b: any) => {
                    const valA = a[sort] ?? '';
                    const valB = b[sort] ?? '';
                    if (valA < valB) return order === 'asc' ? -1 : 1;
                    if (valA > valB) return order === 'asc' ? 1 : -1;
                    return 0;
                });

                const totalCount = list.length;
                const startIndex = (page - 1) * size;
                const paged = list.slice(startIndex, startIndex + size);

                const result: PagedPointage = {
                    pointages: paged,
                    totalCount
                };

                this._pointages.next(paged);
                this._pointagesLength.next(totalCount);
                return result;
            })
        );
    }
}
