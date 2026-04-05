import json

with open('aidetek_box8.json', 'r') as f:
    data = json.load(f)

key_order = ["Name", "Manufacturer", "Manufacturer Part Number", "Location", "SubLocation", "Image", "Description", "Datasheet"]

reordered = [{k: item.get(k, "") for k in key_order} for item in data]

with open('aidetek_box81.json', 'w') as f:
    json.dump(reordered, f, indent=4)

print(f"Done. {len(reordered)} entries reordered.")