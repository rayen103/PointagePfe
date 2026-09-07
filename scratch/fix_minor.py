# Fix circuit list.component.ts duplicate import
circuit_path = r'c:\Users\rayen\RiderProjects\frontend\src\app\modules\cst\circuit\list\list.component.ts'
with open(circuit_path, 'r', encoding='utf-8') as f:
    text = f.read()
lines = text.splitlines(True)
# remove first import { take } from 'rxjs';
new_lines = []
found_take = False
for l in lines:
    if "import { take } from 'rxjs';" in l:
        if not found_take:
            found_take = True
            continue
    new_lines.append(l)

with open(circuit_path, 'w', encoding='utf-8') as f:
    f.write(''.join(new_lines))

# Fix rattachement-article list.component.ts property name
rat_art_path = r'c:\Users\rayen\RiderProjects\frontend\src\app\modules\cst\rattachement-article\list\list.component.ts'
with open(rat_art_path, 'r', encoding='utf-8') as f:
    text2 = f.read()
text2 = text2.replace('rattachementsArticle$', 'rattachementArticle$')
with open(rat_art_path, 'w', encoding='utf-8') as f:
    f.write(text2)

print('FIXES APPLIED OK')
