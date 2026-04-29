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
- [Wichtige Hinweise](#wichtige-hinweise)
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

| Komponente      | Details                                                                       |
| --------------- | ----------------------------------------------------------------------------- |
| Engine          | Unity (Universal Render Pipeline)                                             |
| Sprache         | C#                                                                            |
| Render-Pipeline | URP mit separaten PC- und Mobile-Profilen                                     |
| Plattformen     | PC (primär), Mobile (vorbereitet)                                             |
| Input           | Unity **Legacy Input System** (siehe [Wichtige Hinweise](#wichtige-hinweise)) |

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
│   ├── SampleScene.unity   # Test-/Beispielszene
│   └── main.unity          # Hauptspielszene
│
├── Scripts/
│   ├── Main.cs             # Einstiegspunkt & zentrale Konfiguration
│   │
│   ├── factory/            # Factory-Pattern für Objekt-Erzeugung
│   │   ├── Factory.cs          # Abstrakte Basisklasse
│   │   ├── Block_factory.cs    # Erzeugt Welt-Blöcke (Gras, Wasser)
│   │   └── Detail_Factory.cs   # Erzeugt Details (Bäume, Steine)
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
│   └── Events/             # Interaktions-Systeme
│       ├── InRange.cs          # Prüft ob Spieler in Reichweite ist
│       ├── Clicks/
│       │   └── OnClick.cs      # Mausklick-Handler (Objekte abbauen)
│       └── Hover/
│           └── HoverDetector.cs # Hover-Effekte auf interagierbaren Objekten
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
└── Detail_factory  → "tree", "rock"
```

### Weltgenerierung (`Worldgen`)

- Generiert ein **Grid aus Kacheln** basierend auf `bounds` und `spacing`
- Platziert zufällig **Details** (Bäume / Steine) mit konfigurierbarer Dichte (`detailspread`)
- Legt eine **Wasserfläche** unter dem Terrain an

Konfigurierbare Parameter in `Main.cs` (über den Unity Inspector):

| Parameter      | Beschreibung                              | Standard        |
| -------------- | ----------------------------------------- | --------------- |
| `bounds`       | Halbgröße der Welt (Radius in Kacheln)    | 5               |
| `spacing`      | Abstand zwischen Kacheln                  | 1.1             |
| `Detailspread` | Wahrscheinlichkeit für Details (0–100)    | 20              |
| `tree_size`    | Größe der Baum-Objekte                    | (0.25, 1, 0.25) |
| `rock_size`    | Größe der Fels-Objekte                    | (0.5, 0.5, 0.5) |
| `range`        | Reichweite des Spielers für Interaktionen | 2               |

### Interaktionssystem

Jedes interagierbare Objekt erhält beim Spawn:

- **`OnClick`** – Zerstört das Objekt bei Klick, wenn der Spieler in Reichweite ist, und erhöht den entsprechenden Ressourcenzähler in `Main`
- **`HoverDetector`** – Skaliert das Objekt leicht hoch beim Hovern (visuelle Rückmeldung), nur wenn der Spieler in Reichweite ist
- **`InRange`** – Prüft zur Laufzeit den Abstand zum Spieler

Die **Reichweite** (`range`) wird zentral in `Main.cs` konfiguriert.

### Spieler-System

- **`Spawn_player`** – Erzeugt den Spieler-Cube zur Laufzeit, setzt Tag `"Player"` und verknüpft die Kamera
- **`PlayerHandler`** – Physik-basierte Bewegung via `Rigidbody.MovePosition` und `Input.GetAxis`
- **`FollowPlayer`** – Kamera folgt dem Spieler mit `Vector3.SmoothDamp` und konfigurierbarem Offset

---

## 🎬 Szenenaufbau (`main.unity`)

| GameObject          | Komponenten                                                                      | Beschreibung                                             |
| ------------------- | -------------------------------------------------------------------------------- | -------------------------------------------------------- |
| `Main Camera`       | Camera, AudioListener, UniversalAdditionalCameraData, **Main**, **FollowPlayer** | Hauptkamera, trägt das Main-Script und folgt dem Spieler |
| `Directional Light` | Light, UniversalAdditionalLightData                                              | Hauptlichtquelle                                         |
| `UI`                | Canvas, CanvasScaler, GraphicRaycaster                                           | HUD mit Holz- und Steinzähler                            |
| `EventSystem`       | EventSystem, StandaloneInputModule                                               | Unity UI Event-Handling                                  |

> **Hinweis:** Der Spieler und alle Welt-Objekte werden **dynamisch zur Laufzeit** erzeugt – sie sind nicht in der Szene vorhanden.

---

## 🎨 Render-Pipeline Konfiguration

### PC-Profil (`PC_RPAsset`)

- **Renderer:** Deferred Rendering
- **Shadows:** 4 Kaskaden, 2048px Shadowmap, Soft Shadows aktiviert
- **SSAO:** Aktiviert (ScreenSpaceAmbientOcclusion, Intensity 0.4)
- **Render Scale:** 1.0 (native Auflösung)
- **Reflection Probes:** Blending + Box Projection + Atlas

### Mobile-Profil (`Mobile_RPAsset`)

- **Renderer:** Forward Rendering
- **Shadows:** 1 Kaskade, 1024px Shadowmap, keine Soft Shadows
- **SSAO:** Deaktiviert
- **Render Scale:** 0.8 (leichte Unterabtastung für Performance)

### Post-Processing (`SampleSceneProfile`)

- **Tonemapping:** ACES
- **Bloom:** Threshold 1.0, Intensity 0.25, High Quality Filtering
- **Vignette:** Intensity 0.2
- **Motion Blur:** Vorhanden, aber deaktiviert

---

## ⚠️ Wichtige Hinweise

### Legacy Input System (Pflicht für `PlayerHandler`)

`PlayerHandler.cs` verwendet `Input.GetAxis("Horizontal")` und `Input.GetAxis("Vertical")` aus dem **Unity Legacy Input System**. Das neue **Input System Package** ist in neueren Unity-Projekten standardmäßig aktiv und führt dazu, dass diese Aufrufe einen Laufzeitfehler werfen und der Spieler sich nicht bewegt.

**So prüfst und stellst du es um:**

1. Gehe in Unity zu **Edit → Project Settings → Player**
2. Scrolle runter zu **Other Settings → Configuration**
3. Finde das Feld **Active Input Handling**
4. Stelle es auf **Input Manager (Old)** oder **Both**

> ⚠️ Steht es auf **Input System Package (New)**, funktioniert `PlayerHandler` nicht.  
> Die einfachste Lösung ist **Both** – damit funktionieren sowohl das Legacy-System als auch das neue parallel.

Nach dem Umstellen fordert Unity einen **Neustart des Editors** an – diesen bestätigen.

**Alternativ** kann `PlayerHandler.cs` auf das neue Input System umgeschrieben werden. Dazu müsste das Package `com.unity.inputsystem` installiert und `Input.GetAxis` durch `InputAction`-Callbacks ersetzt werden.

---

## 🐛 Bekannte Probleme & TODOs

### Offene Code-Qualitätsprobleme

- [ ] **`HoverDetector.Update()`** fügt `InRange` jedes Frame hinzu, falls die Komponente fehlt – das gehört einmalig in `Awake()` oder `Start()`
- [ ] **`originalScale != null`** in `HoverDetector` ist bei `Vector3` (Value Type) immer `true` – die Null-Prüfung ist wirkungslos
- [ ] **`FindObjectOfType<Main>()`** wird in mehreren Scripts bei jedem Klick / Frame aufgerufen – Referenz sollte einmalig gecacht werden

### Fehlende Systeme (geplant)

- [ ] **Inventarsystem** – Ressourcen beim Abbauen aufnehmen und persistent speichern
- [ ] **Crafting-System** – Rezepte definieren und Gegenstände herstellen
- [ ] **Ressourcentypen als ScriptableObjects** – Holz, Stein etc. als Daten-Assets statt hardcodierter Werte
- [ ] **Werkzeug-System** – Unterschiedliche Tools für unterschiedliche Ressourcen und Abbauraten
- [ ] **Save/Load** – Spielstand speichern und laden
- [ ] **Szenenverwaltung** – Hauptmenü, Pause, Game Over
- [ ] **Audio** – Feedback beim Abbauen, Umgebungsgeräusche

---

## 📐 Entwicklungsrichtlinien

### Code-Standards

- **Keine direkte `Update()`-Nutzung** ohne Notwendigkeit – Events und Callbacks bevorzugen
- **Factory-Pattern beibehalten** für alle neuen Objekt-Typen
- **ScriptableObjects** für Daten (Ressourcen, Rezepte, Werkzeuge) verwenden
- Kommentare **nur dort wo nötig** – selbsterklärender Code wird bevorzugt
- **`FindObjectOfType`** möglichst vermeiden – Referenzen über Inspektor oder Events übergeben

### Namenskonventionen

| Typ                  | Konvention | Beispiel          |
| -------------------- | ---------- | ----------------- |
| Klassen              | PascalCase | `BlockFactory`    |
| Methoden             | PascalCase | `GenerateWorld()` |
| Private Felder       | camelCase  | `playerTransform` |
| Serialisierte Felder | camelCase  | `bounds`          |

### Empfohlene nächste Schritte

1. `HoverDetector` refactoren (`InRange` in `Awake()` hinzufügen, nicht per `Update()`)
2. `FindObjectOfType`-Aufrufe durch gecachte Referenzen ersetzen
3. Inventarsystem mit ScriptableObjects für Ressourcentypen aufbauen
4. Crafting-Daten als ScriptableObject-Rezepte definieren

---

_Zuletzt aktualisiert: April 2026_
