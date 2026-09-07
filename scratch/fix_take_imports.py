import os

base_dir = r"c:\Users\rayen\RiderProjects\frontend\src\app\modules"

for root, dirs, files in os.walk(base_dir):
    for f in files:
        if f.endswith(".ts") and ("list.component" in f or "utilisateur.component" in f):
            p = os.path.join(root, f)
            with open(p, "r", encoding="utf-8") as file:
                content = file.read()
            
            if "take(" in content and "take," not in content and "take }" not in content and "{ take }" not in content:
                if "import {" in content and "from 'rxjs'" in content:
                    content = content.replace("from 'rxjs'", ", take } from 'rxjs'")
                    content = content.replace("take } , take }", "take }")
                    with open(p, "w", encoding="utf-8") as file:
                        file.write(content)
                    print("Fixed take import in:", p)
