const express = require("express");
const fs = require("fs");
const path = require("path");
const Fuse = require('fuse.js');

const app = express();
app.use(express.json());
app.use(express.static(__dirname));

const PORT = 8000;

const files = [
    path.join(__dirname, "aidetek_box1.json"),
    path.join(__dirname, "aidetek_box2.json"),
    path.join(__dirname, "aidetek_box3.json"),
    path.join(__dirname, "aidetek_box4.json"),
    path.join(__dirname, "aidetek_box5.json"),
    path.join(__dirname, "aidetek_box6.json"),
    path.join(__dirname, "aidetek_box7.json"),
    path.join(__dirname, "aidetek_box8.json")
];

let database = [];
for (const file of files) {
  const data = JSON.parse(fs.readFileSync(file, "utf8"));
  database = database.concat(data); // or: database.push(...data)
}

const fuseOptions = {
    keys: Object.keys(database[0]), // search all fields
    threshold: 0.4,
};

const fuse = new Fuse(database, fuseOptions);

// Return total count so the client knows the bounds
app.get('/api/count', (req, res) => {
    res.json({ count: database.length });
    console.log("database length: " + database.length);
});

// Return a single item by index
app.get('/api/item/:index', (req, res) => {
    const i = parseInt(req.params.index);
    if (i < 0 || i >= database.length) {
        return res.status(404).json({ error: "Index out of range" });
    }
    res.json(database[i]);
    console.log("index requested: " + req.params.index);
});

// Return total count of search results (or full db if no query)
app.get('/api/search/count', (req, res) => {
    const query = req.query.q;
    if (!query) return res.json({ count: database.length });
    const results = fuse.search(query);
    res.json({ count: results.length });
});

// Return a single item from search results by index
app.get('/api/search/item/:index', (req, res) => {
    const query = req.query.q;
    const i = parseInt(req.params.index);
    if (!query) {
        if (i < 0 || i >= database.length) return res.status(404).json({ error: "Out of range" });
        return res.json(database[i]);
    }
    const results = fuse.search(query);
    if (i < 0 || i >= results.length) return res.status(404).json({ error: "Out of range" });
    res.json(results[i].item);
});

app.listen(PORT, '0.0.0.0', () => console.log("Server running"));