let dbCount = 0;      // total number of items, fetched once on load
let cache = {};       // cache fetched items so we don't re-fetch them
let dbIndex = 0;
let startX = 0;
let isDragging = false;
let pxBetweenCards = 10;
let draggingThreshold = 30;
let searchQuery = "";

// Fetch total count on startup
async function init() {
    const q = encodeURIComponent(searchQuery);
    const res = await fetch(`/api/search/count?q=${q}`);
    const data = await res.json();
    dbCount = data.count;
    dbIndex = 0;
    cache = {};  // clear cache on every new search
    await fetchItem(0);
    await renderCards();
}

// Fetch a single item by index, using cache
async function fetchItem(i) {
    if (i < 0 || i >= dbCount) return null;
    if (cache[i]) return cache[i];
    const q = encodeURIComponent(searchQuery);
    const res = await fetch(`/api/search/item/${i}?q=${q}`);
    cache[i] = await res.json();
    return cache[i];
}

addEventListener("DOMContentLoaded", () => { init(); });

// ── Helpers ────────────────────────────────────────────────────────────────

function makeCard(item, index) {
    const card = document.createElement("div");
    card.className = "card";
    card.dataset.index = index;
    const itemName = item["Name"] ? item["Name"] : "No name";
    card.innerHTML = `
        <div class="card-title">${itemName}</div>
        ${Object.entries(item)
            .slice(1) // skip the first field since it's the title
            .filter(([key, val]) => key !== "Image") // skip image
            .map(([key, val]) => `<div class="card-field">${key}: ${val}</div>`)
            .join("")}
    `;
    return card;
}

// Position all cards instantly (no animation) based on dbIndex
function positionCards() {
    const cards = document.querySelectorAll("#cardStack .card");
    const stackWidth = document.getElementById("cardStack").offsetWidth;

    cards.forEach(card => {
        const i = parseInt(card.dataset.index);
        const slot = i - dbIndex; // -1, 0, or 1
        const baseX = slot * (stackWidth + pxBetweenCards);
        card.style.transition = "none";
        card.style.transform = `translateX(${baseX}px)`;
    });
}

// Animate all cards to their final resting position after a swipe
function snapCards(targetIndex) {
    const cards = document.querySelectorAll("#cardStack .card");
    const stackWidth = document.getElementById("cardStack").offsetWidth;

    cards.forEach(card => {
        const i = parseInt(card.dataset.index);
        const slot = i - targetIndex;
        const baseX = slot * (stackWidth + pxBetweenCards);
        card.style.transition = "transform 0.3s ease";
        card.style.transform = `translateX(${baseX}px)`;
    });
}

// ── Render ─────────────────────────────────────────────────────────────────

async function renderCards() {
    const stack = document.getElementById("cardStack");
    stack.innerHTML = "";

    if (dbCount === 0) { updateCounter(); return; }

    // Pre-fetch neighbours before rendering
    await Promise.all([
        fetchItem(dbIndex - 1),
        fetchItem(dbIndex),
        fetchItem(dbIndex + 1),
    ]);

    const indices = [dbIndex - 1, dbIndex, dbIndex + 1];
    indices.forEach(i => {
        if (i >= 0 && i < dbCount) {
            stack.appendChild(makeCard(cache[i], i));
        }
    });

    positionCards();
    attachSwipeListeners();
    updateCounter();
}

// ── Swipe ──────────────────────────────────────────────────────────────────

function attachSwipeListeners() {
    const stack = document.getElementById("cardStack");

    // Clone to remove any old listeners
    const fresh = stack.cloneNode(true);
    stack.parentNode.replaceChild(fresh, stack);

    fresh.addEventListener("touchstart", e => {
        startX = e.touches[0].clientX;
        // Disable transitions on all cards during drag
        fresh.querySelectorAll(".card").forEach(c => c.style.transition = "none");
        isDragging = false;
    }, { passive: true });

    fresh.addEventListener("touchmove", e => {
        if (Math.abs(e.touches[0].clientX - startX) > draggingThreshold) {
            isDragging = true;
        }
        if (!isDragging) return;
        const diff = e.touches[0].clientX - startX;
        const stackWidth = fresh.offsetWidth;

        fresh.querySelectorAll(".card").forEach(card => {
            const i = parseInt(card.dataset.index);
            const slot = i - dbIndex;
            const baseX = slot * (stackWidth + pxBetweenCards);
            card.style.transform = `translateX(${baseX + diff}px)`;
        });
    }, { passive: true });

    fresh.addEventListener("touchend", async e => {
        if (!isDragging) {
            openOverlay(cache[dbIndex].Image);
            // Don't run swipe logic if dragging
            return;
        };
        isDragging = false;

        const diff = e.changedTouches[0].clientX - startX;

        if (diff < -draggingThreshold && dbIndex < dbCount - 1) {
            // Swiped left → go to next
            dbIndex++;
            await ensureNeighboursExist(fresh);
            snapCards(dbIndex);
        } else if (diff > draggingThreshold && dbIndex > 0) {
            // Swiped right → go to prev
            dbIndex--;
            await ensureNeighboursExist(fresh);
            snapCards(dbIndex);
        } else {
            // Not far enough → snap back
            snapCards(dbIndex);
        }

        updateCounter();
    }, { passive: true });
}

async function ensureNeighboursExist(stack) {
    // Remove cards outside N-1, N, N+1
    stack.querySelectorAll(".card").forEach(card => {
        const i = parseInt(card.dataset.index);
        if (Math.abs(i - dbIndex) > 1) card.remove();
    });

    // Pre-fetch before inserting
    await Promise.all([
        fetchItem(dbIndex - 1),
        fetchItem(dbIndex),
        fetchItem(dbIndex + 1),
    ]);

    [dbIndex - 1, dbIndex, dbIndex + 1].forEach(i => {
        if (i < 0 || i >= dbCount) return;
        if (!stack.querySelector(`.card[data-index="${i}"]`)) {
            const card = makeCard(cache[i], i);
            const stackWidth = stack.offsetWidth;
            const direction = i - dbIndex;
            card.style.transition = "none";
            card.style.transform = `translateX(${direction * 2 * stackWidth}px)`;
            stack.appendChild(card);
        }
    });
}

// ── Misc ───────────────────────────────────────────────────────────────────

function updateCounter() {
    document.getElementById("cardCounter").textContent =
        dbCount > 0 ? `${dbIndex + 1} of ${dbCount}` : "";
}

document.getElementById("searchBox").addEventListener("input", function () {
    searchQuery = this.value;
    init();
});

function clearSearch() {
    document.getElementById("searchBox").value = "";
    searchQuery = "";
    init();
}

let overlayOpenedAt = 0;

function openOverlay(imageSrc) {
    const overlay = document.getElementById("overlay");
    const img = document.getElementById("overlayImage");
    img.src = imageSrc;
    overlay.classList.add("active");
    overlayOpenedAt = Date.now();
}

function closeOverlay() {
    if (Date.now() - overlayOpenedAt < 300) return; // ignore the tap that opened it
    const overlay = document.getElementById("overlay");
    overlay.classList.remove("active");
}

document.getElementById("overlay").addEventListener("click", closeOverlay);