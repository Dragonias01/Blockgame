# 🌍 Oasis – Unity Survival & Crafting Game

> Ein Open-World Survival-Crafting-Spiel auf Basis von Unity (URP), entwickelt in C#.  
> Der Spieler erkundet eine prozedural generierte Welt, sammelt Ressourcen und verarbeitet sie zu nützlichen Gegenständen.

---

## 📋 Inhaltsverzeichnis

- [Spielprinzip](#spielprinzip)
- [Technischer Stack](#technischer-stack)
- [Projektstruktur](#projektstruktur)
- [Systeme & Architektur](#systeme--architektur)
- [Szenenaufbau](#szenenaufbau)
- [Render-Pipeline Konfiguration](#render-pipeline-konfiguration)
- [Bekannte Probleme & TODOs](#bekannte-probleme--todos)
- [Entwicklungsrichtlinien](#entwicklungsrichtlinien)

---

## 🎮 Spielprinzip

Der Spieler startet in einer prozedural generierten Welt und kann:

- **Ressourcen sammeln** – Holz (Bäume), Stein (Felsen), Wasser und weitere Materialien abbauen
- **Crafting & Verarbeitung** – Ressourcen zu Werkzeugen, Gebäuden oder weiterverarbeiteten Materialien umwandeln _(in Entwicklung)_
- **Progression erleben** – Bessere Werkzeuge ermöglichen effizienteres Sammeln und neue Rezepte _(geplant)_

Das Spiel orientiert sich am klassischen **Survival-Crafting-Loop**: Sammeln → Verarbeiten → Bauen → Überleben.

---

## 🛠️ Technischer Stack

| Komponente      | Details                                   |
| --------------- | ----------------------------------------- |
| Engine          | Unity (Universal Render Pipeline)         |
| Sprache         | C#                                        |
| Render-Pipeline | URP mit separaten PC- und Mobile-Profilen |
| Plattformen     | PC (primär), Mobile (vorbereitet)         |
| Input           | Unity Legacy Input System                 |

---

## 📁 Projektstruktur

```
Assets/
├── Resources/
│   ├── Materials/          # Materialien (grass1, grass2, wasser1)
│   ├── Prefabs/            # Prefabs (z.B. stone1)
│   ├── grass1.jpg          # Gras-Textur
│   └── wasser1.jpg         # Wasser-Textur
│
├── Scenes/
│   └── SampleScene.unity   # Hauptszene
│
├── Scripts/
│   ├── Main.cs             # Einstiegspunkt & Konfiguration
│   │
│   ├── factory/            # Factory-Pattern für Objekt-Erzeugung
│   │   ├── Factory.cs          # Abstrakte Basisklasse
│   │   ├── Block_factory.cs    # Erzeugt Welt-Blöcke (Gras, Wasser)
│   │   ├── Detail_Factory.cs   # Erzeugt Details (Bäume, Steine)
│   │   └── UI_Factory.cs       # Erzeugt UI-Elemente
│   │
│   ├── worldgen/           # Weltgenerierung
│   │   ├── Worldgen.cs         # Hauptlogik der prozeduralen Weltgenerierung
│   │   ├── Block.cs            # Abstrakte Basisklasse für alle Blöcke
│   │   ├── block/
│   │   │   ├── Default_block.cs    # Standard-Geländekachel (Gras)
│   │   │   └── WaterPlane.cs       # Wasserfläche
│   │   └── detail/
│   │       ├── Tree.cs             # Baum-Detail (sammelbar)
│   │       └── Rock.cs             # Fels-Detail (sammelbar)
│   │
│   ├── Player/             # Spieler-Systeme
│   │   ├── PlayerHandler.cs    # Bewegungssteuerung (WASD / Achsen)
│   │   ├── FollowPlayer.cs     # Kamera folgt dem Spieler (SmoothDamp)
│   │   └── Spawn_player.cs     # Spawnt den Spieler beim Start
│   │
│   ├── Events/             # Interaktions-Systeme
│   │   ├── InRange.cs          # Prüft ob Spieler in Reichweite ist
│   │   ├── Clicks/
│   │   │   └── OnClick.cs      # Mausklick-Handler (Objekte abbauen)
│   │   └── Hover/
│   │       └── HoverDetector.cs # Hover-Effekte auf interagierbaren Objekten
│   │
│   └── UI/                 # Benutzeroberfläche
│       ├── UI_Element.cs       # Abstrakte Basisklasse für UI-Elemente
│       ├── UIHandler.cs        # Erstellt und verwaltet UI
│       ├── UserInterface.cs    # Alternative UI-Erstellung (Button-Beispiel)
│       └── Element/
│           ├── UI_Canvas.cs    # Canvas-Element
│           └── UI_Text.cs      # Text-Element
│
└── Settings/               # URP Render-Pipeline Konfigurationen
    ├── PC_RPAsset.asset        # PC-Qualitätsprofil (hohe Qualität)
    ├── PC_Renderer.asset       # PC-Renderer (Deferred + SSAO)
    ├── Mobile_RPAsset.asset    # Mobile-Qualitätsprofil (optimiert)
    ├── Mobile_Renderer.asset   # Mobile-Renderer (Forward)
    ├── SampleSceneProfile.asset    # Post-Processing Profil der Szene
    └── DefaultVolumeProfile.asset  # Standard Volume-Einstellungen
```

---

## ⚙️ Systeme & Architektur

### Factory-Pattern

Alle Spielobjekte werden über **Factories** erzeugt, nicht direkt instanziiert. Das ermöglicht einfaches Erweitern ohne bestehenden Code anzufassen.

```
Factory (abstract)
├── Block_factory   → "default1", "water"
├── Detail_factory  → "tree", "rock"
└── UI_Factory      → "canvas", "text"
```

### Weltgenerierung (`Worldgen`)

- Generiert ein **Grid aus Kacheln** basierend auf `bounds` und `spacing`
- Platziert zufällig **Details** (Bäume / Steine) mit konfigurierbarer Dichte (`detailspread`)
- Legt eine **Wasserfläche** unter dem Terrain an

Konfigurierbare Parameter in `Main.cs` (über den Unity Inspector):

| Parameter      | Beschreibung                           | Standard |
| -------------- | -------------------------------------- | -------- |
| `bounds`       | Halbgröße der Welt (Radius in Kacheln) | 5        |
| `spacing`      | Abstand zwischen Kacheln               | 1.1      |
| `Detailspread` | Wahrscheinlichkeit für Details (0–100) | 20       |

### Interaktionssystem

Jedes interagierbare Objekt erhält beim Spawn:

- **`OnClick`** – Zerstört das Objekt bei Klick, wenn der Spieler in Reichweite ist
- **`HoverDetector`** – Skaliert das Objekt leicht hoch beim Hovern (visuelle Rückmeldung)
- **`InRange`** – Prüft zur Laufzeit den Abstand zum Spieler

Die **Reichweite** (`range`) wird zentral in `Main.cs` konfiguriert.

### Spieler-System

- **`Spawn_player`** – Erzeugt den Spieler-Cube, setzt Tag `"Player"` und verknüpft die Kamera
- **`PlayerHandler`** – Physik-basierte Bewegung via `Rigidbody.MovePosition`
- **`FollowPlayer`** – Kamera folgt dem Spieler mit `SmoothDamp` und konfigurierbarem Offset

---

## 🎬 Szenenaufbau (`SampleScene`)

| GameObject          | Komponenten                                                            | Beschreibung                   |
| ------------------- | ---------------------------------------------------------------------- | ------------------------------ |
| `Main Camera`       | Camera, AudioListener, UniversalAdditionalCameraData, **FollowPlayer** | Hauptkamera, folgt dem Spieler |
| `Directional Light` | Light, UniversalAdditionalLightData                                    | Hauptlichtquelle               |
| `Global Volume`     | Volume (global)                                                        | Post-Processing für die Szene  |

> **Hinweis:** Der Spieler und alle Welt-Objekte werden **dynamisch zur Laufzeit** erzeugt – sie sind nicht in der Szene vorhanden.

Ein `Main`-MonoBehaviour muss als Komponente auf einem GameObject in der Szene liegen, um das Spiel zu starten.

---

## 🎨 Render-Pipeline Konfiguration

### PC-Profil (`PC_RPAsset`)

- **Renderer:** Deferred Rendering
- **Shadows:** 4 Kaskaden, 2048px Shadowmap, Soft Shadows
- **SSAO:** Aktiviert (ScreenSpaceAmbientOcclusion)
- **Render Scale:** 1.0 (native Auflösung)
- **Reflection Probes:** Blending + Box Projection + Atlas

### Mobile-Profil (`Mobile_RPAsset`)

- **Renderer:** Forward Rendering
- **Shadows:** 1 Kaskade, 1024px Shadowmap, keine Soft Shadows
- **SSAO:** Deaktiviert
- **Render Scale:** 0.8 (leichte Unterabtastung für Performance)

### Post-Processing (SampleSceneProfile)

- **Tonemapping:** ACES
- **Bloom:** Threshold 1.0, Intensity 0.25, High Quality Filtering
- **Vignette:** Intensity 0.2
- **Motion Blur:** Vorhanden, aber deaktiviert

---

## 🐛 Bekannte Probleme & TODOs

### Bugs / Code-Qualität

- [ ] `Spawn_player` erbt fälschlicherweise von `Main` – sollte eine eigenständige Klasse sein
- [ ] `HoverDetector` fügt `InRange` per `Update()` hinzu – sollte in `Awake()` oder `Start()` geschehen
- [ ] `Detail_factory` fügt `HoverDetector` doppelt hinzu (zwei `AddComponent<HoverDetector>()` Zeilen)
- [ ] `Color.lavenderBlush` existiert nicht in Unity – führt zu Compile-Fehler in `Spawn_player.cs`
- [ ] `UI_Text` verwendet veraltetes `Text`-UI-System (Legacy) statt `TextMeshPro`
- [ ] `originalScale != null` in `HoverDetector` ist bei `Vector3` immer true (Value Type)

### Fehlende Systeme (geplant)

- [ ] **Inventarsystem** – Ressourcen beim Abbauen aufnehmen und speichern
- [ ] **Crafting-System** – Rezepte definieren und Gegenstände herstellen
- [ ] **Ressourcen-Typen** – Holz, Stein etc. als Daten (ScriptableObjects empfohlen)
- [ ] **Werkzeug-System** – Unterschiedliche Tools für unterschiedliche Ressourcen
- [ ] **Save/Load** – Spielstand speichern und laden
- [ ] **Szenenverwaltung** – Menü, Pause, Game Over
- [ ] **Sound** – Feedback beim Abbauen, Ambient-Sounds

---

## 📐 Entwicklungsrichtlinien

### Code-Standards

- **Keine direkte `Update()`-Nutzung** ohne Notwendigkeit – Events und Callbacks bevorzugen
- **Factory-Pattern beibehalten** für alle neuen Objekt-Typen
- **ScriptableObjects** für Daten (Ressourcen, Rezepte, Werkzeuge) verwenden
- Kommentare **nur dort wo nötig** – selbsterklärender Code wird bevorzugt
- **`FindObjectOfType`** möglichst vermeiden – stattdessen Referenzen über Inspektor oder Events übergeben

### Namenskonventionen

- Klassen: `PascalCase` (z.B. `BlockFactory`)
- Methoden: `PascalCase` (z.B. `GenerateWorld`)
- Private Felder: `camelCase` mit Unterstrich (z.B. `_playerTransform`)
- Serialisierte Felder: `camelCase` (z.B. `bounds`)

### Empfohlene nächste Schritte

1. Bug-Fixes aus der obigen Liste abarbeiten (insbesondere `Spawn_player` und `HoverDetector`)
2. Inventarsystem mit ScriptableObjects für Ressourcentypen aufbauen
3. UI auf TextMeshPro umstellen
4. Crafting-Daten als ScriptableObject-Rezepte definieren

---

_Zuletzt aktualisiert: April 2026_
