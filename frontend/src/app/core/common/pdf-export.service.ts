import { Injectable } from '@angular/core';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

export interface PdfColumn {
    header: string;
    dataKey: string;
}

@Injectable({
    providedIn: 'root'
})
export class PdfExportService {
    exportToPdf(title: string, columns: PdfColumn[], data: any[], fileName?: string): void {
        if (!data || data.length === 0) {
            console.warn('No data available to export to PDF');
            return;
        }

        const doc = new jsPDF('p', 'mm', 'a4');

        // Header decoration banner
        doc.setFillColor(37, 99, 235);
        doc.rect(0, 0, 210, 20, 'F');
        
        doc.setFont('helvetica', 'bold');
        doc.setFontSize(14);
        doc.setTextColor(255, 255, 255);
        doc.text(title.toUpperCase(), 14, 13);

        // Subtitle with export timestamp
        doc.setFontSize(9);
        doc.setFont('helvetica', 'normal');
        doc.setTextColor(100, 116, 139);
        const dateStr = `Rapport d'exportation généré le : ${new Date().toLocaleDateString('fr-FR')} à ${new Date().toLocaleTimeString('fr-FR')}`;
        doc.text(dateStr, 14, 27);

        // Render table
        autoTable(doc, {
            startY: 31,
            head: [columns.map(c => c.header)],
            body: data.map(row => columns.map(c => {
                const val = row[c.dataKey];
                if (val === null || val === undefined) return '';
                if (typeof val === 'boolean') return val ? 'Oui' : 'Non';
                return String(val);
            })),
            styles: {
                font: 'helvetica',
                fontSize: 9,
                cellPadding: 3,
            },
            headStyles: {
                fillColor: [37, 99, 235],
                textColor: [255, 255, 255],
                fontStyle: 'bold',
            },
            alternateRowStyles: {
                fillColor: [248, 250, 252],
            },
            margin: { top: 31, left: 14, right: 14 },
        });

        const targetFileName = fileName || `${title.toLowerCase()}_export_${new Date().toISOString().slice(0, 10)}.pdf`;
        doc.save(targetFileName);
    }
}
