import os, re

base_dir = r"c:\Users\rayen\RiderProjects\frontend\src\app\modules"

# Configuration dictionary mapping module paths to Entity Names and Columns
entities_config = {
    "cst/bus": {
        "title": "Rapport Flotte des Bus",
        "fileName": "Bus_Export.pdf",
        "service": "_busService",
        "obs": "buses$",
        "alt_obs": "bus$",
        "columns": [
            {"header": "Immatriculation", "dataKey": "numeroIMM"},
            {"header": "Modèle", "dataKey": "modelBus"},
            {"header": "Capacité", "dataKey": "capacite"},
            {"header": "Occupants", "dataKey": "currentOccupancy"},
            {"header": "Code Circuit", "dataKey": "codeCircuit"},
            {"header": "Statut Actif", "dataKey": "isActive"}
        ]
    },
    "cst/chauffeur": {
        "title": "Rapport Chauffeurs",
        "fileName": "Chauffeurs_Export.pdf",
        "service": "_chauffeurService",
        "obs": "chauffeurs$",
        "columns": [
            {"header": "Code Chauffeur", "dataKey": "codeChauffeur"},
            {"header": "Nom", "dataKey": "nom"},
            {"header": "Prénom", "dataKey": "prenom"},
            {"header": "Téléphone", "dataKey": "telephone"},
            {"header": "Permis", "dataKey": "numeroPermis"}
        ]
    },
    "cst/modem": {
        "title": "Rapport Modems",
        "fileName": "Modems_Export.pdf",
        "service": "_modemService",
        "obs": "modems$",
        "columns": [
            {"header": "N° IMEI", "dataKey": "numeroIMEI"},
            {"header": "Véhicule", "dataKey": "codeVehicule"},
            {"header": "Libellé", "dataKey": "libelle"},
            {"header": "Statut", "dataKey": "isActive"}
        ]
    },
    "cst/circuit": {
        "title": "Rapport Circuits de Collecte",
        "fileName": "Circuits_Export.pdf",
        "service": "_circuitService",
        "obs": "circuits$",
        "alt_obs": "circuit$",
        "columns": [
            {"header": "Code Circuit", "dataKey": "codeCircuit"},
            {"header": "Libellé", "dataKey": "libelle"},
            {"header": "Distance (km)", "dataKey": "distance"},
            {"header": "Statut", "dataKey": "isActive"}
        ]
    },
    "cst/pointcollecte": {
        "title": "Rapport Points de Collecte",
        "fileName": "PointsCollecte_Export.pdf",
        "service": "_pointCollecteService",
        "obs": "pointsCollecte$",
        "columns": [
            {"header": "Code Point", "dataKey": "codePointCollecte"},
            {"header": "Libellé", "dataKey": "libelle"},
            {"header": "Latitude", "dataKey": "latitude"},
            {"header": "Longitude", "dataKey": "longitude"}
        ]
    },
    "cst/pointage": {
        "title": "Rapport des Pointages",
        "fileName": "Pointages_Export.pdf",
        "service": "_pointageService",
        "obs": "pointages$",
        "columns": [
            {"header": "Code Employé", "dataKey": "codeEmploye"},
            {"header": "Date Pointage", "dataKey": "datePointage"},
            {"header": "Statut", "dataKey": "statut"}
        ]
    },
    "cst/region": {
        "title": "Rapport Régions",
        "fileName": "Regions_Export.pdf",
        "service": "_regionService",
        "obs": "regions$",
        "columns": [
            {"header": "Code Région", "dataKey": "codeRegion"},
            {"header": "Libellé Région", "dataKey": "libelleRegion"},
            {"header": "Code Gouvernorat", "dataKey": "codeGouvernorat"}
        ]
    },
    "cst/gouvernorat": {
        "title": "Rapport Gouvernorats",
        "fileName": "Gouvernorats_Export.pdf",
        "service": "_gouvernoratService",
        "obs": "gouvernorats$",
        "columns": [
            {"header": "Code Gouvernorat", "dataKey": "codeGouvernorat"},
            {"header": "Libellé Gouvernorat", "dataKey": "libelleGouvernorat"}
        ]
    },
    "cst/shift": {
        "title": "Rapport Shifts de Travail",
        "fileName": "Shifts_Export.pdf",
        "service": "_shiftService",
        "obs": "shifts$",
        "columns": [
            {"header": "Code Shift", "dataKey": "codeShift"},
            {"header": "Libellé", "dataKey": "libelleShift"},
            {"header": "Heure Début", "dataKey": "heureDebut"},
            {"header": "Heure Fin", "dataKey": "heureFin"}
        ]
    },
    "cst/equipe": {
        "title": "Rapport Équipes",
        "fileName": "Equipes_Export.pdf",
        "service": "_equipeService",
        "obs": "equipes$",
        "columns": [
            {"header": "Code Équipe", "dataKey": "codeEquipe"},
            {"header": "Libellé Équipe", "dataKey": "libelleEquipe"}
        ]
    },
    "cst/rattachement": {
        "title": "Rapport Rattachements",
        "fileName": "Rattachements_Export.pdf",
        "service": "_rattachementService",
        "obs": "rattachements$",
        "columns": [
            {"header": "Code Rattachement", "dataKey": "codeRattachement"},
            {"header": "Code Employé", "dataKey": "codeEmploye"},
            {"header": "Code Bus", "dataKey": "codeBus"},
            {"header": "Code Circuit", "dataKey": "codeCircuit"}
        ]
    },
    "cst/rattachement-article": {
        "title": "Rapport Rattachements Articles",
        "fileName": "RattachementArticles_Export.pdf",
        "service": "_rattachementArticleService",
        "obs": "rattachementArticle$",
        "columns": [
            {"header": "Code Article", "dataKey": "codeArticle"},
            {"header": "Désignation", "dataKey": "designation"},
            {"header": "Quantité", "dataKey": "quantite"}
        ]
    },
    "cst/rattachement-employe": {
        "title": "Rapport Rattachements Employés",
        "fileName": "RattachementEmployes_Export.pdf",
        "service": "_rattachementEmployeService",
        "obs": "rattachementEmployes$",
        "columns": [
            {"header": "Code Employé", "dataKey": "codeEmploye"},
            {"header": "Matricule", "dataKey": "matricule"},
            {"header": "Nom", "dataKey": "nom"},
            {"header": "Prénom", "dataKey": "prenom"}
        ]
    },
    "cst/societe": {
        "title": "Rapport Sociétés",
        "fileName": "Societes_Export.pdf",
        "service": "_societeService",
        "obs": "societes$",
        "columns": [
            {"header": "Code Société", "dataKey": "codeSociete"},
            {"header": "Raison Sociale", "dataKey": "raisonSociale"},
            {"header": "Adresse", "dataKey": "adresse"},
            {"header": "Téléphone", "dataKey": "telephone"}
        ]
    },
    "cst/chantier": {
        "title": "Rapport Chantiers",
        "fileName": "Chantiers_Export.pdf",
        "service": "_chantierService",
        "obs": "chantiers$",
        "columns": [
            {"header": "Code Chantier", "dataKey": "codeChantier"},
            {"header": "Libellé Chantier", "dataKey": "libelleChantier"},
            {"header": "Code Société", "dataKey": "codeSociete"}
        ]
    },
    "cst/ordretravail": {
        "title": "Rapport Ordres de Travail",
        "fileName": "OrdresTravail_Export.pdf",
        "service": "_ordreTravailService",
        "obs": "ordresTravail$",
        "columns": [
            {"header": "N° OT", "dataKey": "numeroOrdreTravail"},
            {"header": "Chantier", "dataKey": "numeroChantier"},
            {"header": "Véhicule", "dataKey": "codeVehicule"},
            {"header": "Équipe", "dataKey": "codeEquipe"},
            {"header": "État OT", "dataKey": "etatOT"}
        ]
    },
    "gestion-employe/employe": {
        "title": "Rapport Liste des Employés",
        "fileName": "Employes_Export.pdf",
        "service": "_employeService",
        "obs": "employes$",
        "columns": [
            {"header": "Matricule", "dataKey": "matricule"},
            {"header": "Nom", "dataKey": "nom"},
            {"header": "Prénom", "dataKey": "prenom"},
            {"header": "CIN", "dataKey": "cin"},
            {"header": "Code Équipe", "dataKey": "codeEquipe"},
            {"header": "Statut Actif", "dataKey": "isActive"}
        ]
    },
    "gestion-utilisateur/utilisateur": {
        "title": "Rapport Liste des Utilisateurs",
        "fileName": "Utilisateurs_Export.pdf",
        "service": "_utilisateurService",
        "obs": "utilisateurs$",
        "columns": [
            {"header": "Nom Utilisateur", "dataKey": "nomUtilisateur"},
            {"header": "Nom", "dataKey": "nom"},
            {"header": "Prénom", "dataKey": "prenom"},
            {"header": "Email", "dataKey": "email"},
            {"header": "Rôle", "dataKey": "role"},
            {"header": "Actif", "dataKey": "isActive"}
        ]
    }
}

print("Starting PDF export update across all entities...")

for rel_path, cfg in entities_config.items():
    target_folder = os.path.join(base_dir, rel_path.replace("/", os.sep))
    if not os.path.exists(target_folder):
        continue

    for root, dirs, files in os.walk(target_folder):
        for f in files:
            # 1. Update HTML files
            if f.endswith(".html") and ("list.component" in f or "utilisateur.component" in f):
                html_path = os.path.join(root, f)
                with open(html_path, "r", encoding="utf-8") as hf:
                    html_content = hf.read()
                
                # Replace button content with Exporter PDF and document-text icon
                pattern = r'<button class="cst-btn cst-btn--ghost"[^>]*>.*?</button>'
                replacement = f'''<button class="cst-btn cst-btn--ghost" type="button" (click)="exportData()">
                    <mat-icon class="cst-btn__i" [svgIcon]="'heroicons_outline:document-text'"></mat-icon>
                    Exporter PDF
                </button>'''
                
                new_html = re.sub(pattern, replacement, html_content, flags=re.DOTALL)
                if new_html != html_content:
                    with open(html_path, "w", encoding="utf-8") as hf:
                        hf.write(new_html)
                    print(f"Updated HTML: {html_path}")

            # 2. Update TS files
            if f.endswith(".ts") and ("list.component" in f or "utilisateur.component" in f):
                ts_path = os.path.join(root, f)
                with open(ts_path, "r", encoding="utf-8") as tf:
                    ts_content = tf.read()

                # Add PdfExportService import if missing
                csv_service_rel = os.path.relpath(
                    r"c:\Users\rayen\RiderProjects\frontend\src\app\core\common\pdf-export.service",
                    os.path.dirname(ts_path)
                ).replace("\\", "/")
                if not csv_service_rel.startswith("."):
                    csv_service_rel = "./" + csv_service_rel

                if "PdfExportService" not in ts_content:
                    ts_content = f"import {{ PdfExportService }} from '{csv_service_rel}';\n" + ts_content

                # Inject _pdfExportService into constructor if missing
                if "_pdfExportService: PdfExportService" not in ts_content and "constructor(" in ts_content:
                    ts_content = ts_content.replace(
                        "constructor(",
                        "constructor(\n        private _pdfExportService: PdfExportService,\n"
                    )

                # Format cols string
                cols_json = "[\n" + ",\n".join([f"            {{ header: '{c['header']}', dataKey: '{c['dataKey']}' }}" for c in cfg['columns']]) + "\n        ]"
                
                service_name = cfg['service']
                obs_primary = cfg['obs']
                obs_alt = cfg.get('alt_obs', obs_primary)
                title = cfg['title']
                file_name = cfg['fileName']

                new_method = f'''
    exportData(): void {{
        if (this.{service_name}) {{
            const obs$ = (this as any).{obs_primary.replace('$', '')}$ || this.{service_name}.{obs_primary} || this.{service_name}.{obs_alt};
            if (obs$) {{
                obs$.pipe(take(1)).subscribe((data: any) => {{
                    const items = Array.isArray(data) ? data : (data?.items || data?.{obs_primary.replace('$', '')} || data?.{obs_alt.replace('$', '')} || []);
                    if (items && items.length > 0) {{
                        const columns = {cols_json};
                        this._pdfExportService.exportToPdf('{title}', columns, items, '{file_name}');
                    }} else {{
                        console.warn('No data available to export to PDF');
                    }}
                }});
            }}
        }}
    }}
'''
                # Replace existing exportData method
                if "exportData(): void {" in ts_content:
                    method_pattern = r'exportData\(\): void \{.*?\n    \}'
                    ts_content = re.sub(method_pattern, new_method.strip(), ts_content, flags=re.DOTALL)
                else:
                    # Append method before last closing brace
                    last_brace = ts_content.rfind("}")
                    if last_brace != -1:
                        ts_content = ts_content[:last_brace] + new_method + "\n}"

                with open(ts_path, "w", encoding="utf-8") as tf:
                    tf.write(ts_content)
                print(f"Updated TS: {ts_path}")

print("PDF export update completed successfully!")
