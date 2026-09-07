import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class CsvExportService {
    exportToCsv(fileName: string, rows: Record<string, any>[]): void {
        if (!rows || rows.length === 0) {
            console.warn('No data available to export');
            return;
        }

        // Extract keys, filtering out null/functions/objects unless date
        const sample = rows[0];
        const keys = Object.keys(sample).filter(k => {
            const val = sample[k];
            return val === null || val === undefined || typeof val !== 'object' || val instanceof Date;
        });

        if (keys.length === 0) {
            return;
        }

        const headerRow = keys.join(';');
        const dataRows = rows.map(row => 
            keys.map(k => {
                let val = row[k];
                if (val === null || val === undefined) {
                    return '""';
                }
                if (val instanceof Date) {
                    val = val.toISOString();
                }
                const strVal = String(val).replace(/"/g, '""');
                return `"${strVal}"`;
            }).join(';')
        );

        const csvContent = '\uFEFF' + [headerRow, ...dataRows].join('\n');
        const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
        const link = document.createElement('a');
        const url = URL.createObjectURL(blob);
        
        const timestamp = new Date().toISOString().slice(0, 10);
        link.setAttribute('href', url);
        link.setAttribute('download', `${fileName}_export_${timestamp}.csv`);
        link.style.visibility = 'hidden';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }
}
