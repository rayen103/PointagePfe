import os, re

base = r"c:\Users\rayen\RiderProjects\frontend\src\app\modules"

mapping = [
    ("cst\\bus", "Bus", "_busService", "bus$"),
    ("cst\\chantier", "Chantier", "_chantierService", "chantiers$"),
    ("cst\\chauffeur", "Chauffeur", "_chauffeurService", "chauffeurs$"),
    ("cst\\circuit", "Circuit", "_circuitService", "circuit$"),
    ("cst\\equipe", "Equipe", "_equipeService", "equipes$"),
    ("cst\\gouvernorat", "Gouvernorat", "_gouvernoratService", "gouvernorats$"),
    ("cst\\modem", "Modem", "_modemService", "modems$"),
    ("cst\\ordretravail", "OrdreTravail", "_ordreTravailService", "ordresTravail$"),
    ("cst\\pointage", "Pointage", "_pointageService", "pointages$"),
    ("cst\\rattachement-article", "RattachementArticle", "_rattachementArticleService", "rattachementsArticle$"),
    ("cst\\rattachement-employe", "RattachementEmploye", "_rattachementEmployeService", "rattachementEmployes$"),
    ("cst\\rattachement", "Rattachement", "_rattachementService", "rattachements$"),
    ("cst\\region", "Region", "_regionService", "regions$"),
    ("cst\\shift", "Shift", "_shiftService", "shifts$"),
    ("cst\\societe", "Societe", "_societeService", "societes$"),
    ("gestion-employe\\employe", "Employe", "_employeService", "employes$"),
    ("gestion-utilisateur\\utilisateur", "Utilisateur", "_utilisateurService", "utilisateurs$"),
]

for sub, name, serv, obs in mapping:
    for root, dirs, files in os.walk(base):
        if sub.lower() in root.lower():
            for f in files:
                if f.endswith(".ts") and ("list.component" in f or "utilisateur.component" in f):
                    ts_path = os.path.join(root, f)
                    with open(ts_path, "r", encoding="utf-8") as file:
                        content = file.read()
                    
                    if "CsvExportService" in content:
                        continue
                    
                    # Compute relative import path from ts_path to csv-export.service
                    csv_path = r"c:\Users\rayen\RiderProjects\frontend\src\app\core\common\csv-export.service"
                    rel_dir = os.path.relpath(csv_path, os.path.dirname(ts_path)).replace("\\", "/")
                    if not rel_dir.startswith("."):
                        rel_dir = "./" + rel_dir
                    
                    import_stmt = f"import {{ CsvExportService }} from '{rel_dir}';\nimport {{ take }} from 'rxjs';\n"
                    content = import_stmt + content
                    
                    if "constructor(" in content:
                        content = content.replace("constructor(", "constructor(\n        private _csvExportService: CsvExportService,\n")
                    
                    clean_obs = obs.replace("$", "")
                    method_code = f"""
    exportData(): void {{
        if (this.{serv} && this.{serv}.{obs}) {{
            this.{serv}.{obs}.pipe(take(1)).subscribe((data: any) => {{
                const items = Array.isArray(data) ? data : (data?.items || data?.{clean_obs} || []);
                if (items && items.length > 0) {{
                    this._csvExportService.exportToCsv('{name}_Export', items);
                }} else {{
                    console.warn('No data available to export');
                }}
            }});
        }}
    }}
}}
"""
                    idx = content.rfind("}")
                    if idx != -1:
                        content = content[:idx] + method_code
                        with open(ts_path, "w", encoding="utf-8") as file:
                            file.write(content)
                        print("Updated TS:", ts_path)
