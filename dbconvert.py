import re
import json

with open("database.txt", "r", encoding="utf-8") as f:
    text = f.read()

entries = re.findall(r'\{(.*?)\};', text, re.DOTALL)

rows = []
for entry in entries:
    fields = re.findall(r"'([^']*)'='([^']*)'", entry)
    row = {key: value for key, value in fields}
    rows.append(row)

with open("database.json", "w", encoding="utf-8") as f:
    json.dump(rows, f, indent=4)

print(f"Converted {len(rows)} entries to database.json")