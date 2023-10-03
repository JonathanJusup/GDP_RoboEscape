# GDP_RoboEscape - PoC Präsentation

## Ablauf
- Level_01 - Level_02 - Intro - Sandbox

## Level_01
### Playthrough
- Laufe nach rechts
- Schiebe Crate auf Button, warte -> Roter Laser rotiert gegen Uhrzeigersinn
	- Lass Block schmelzen
	- lass PlayerProps zerstören
- Implikation: Roter Laser = Tod
- schiebe Crate nach rechts auf rotes Glas -> Laser wird blockiert
- schiebe Crate nach links zwischen Button und rotes Glas
- springe auf Crate auf Plattform, drücke Button, warte
	- Spiegel dreht sich im Uhrzeigersinn
	- Laser scheint auf Switch -> Tür öffnet sich
 - Laufe durch Tür -> nächstes Level

### Anmerkungen
- Kameras beobachten Spieler, solange Sichtkontakt besteht
- Crate zum Traversieren des Levels + Blockieren von Lasern
- PlayerProps werden durch rote Laser zerstört
	- Environmental Stroytelling + Mechanik Erklärung (Rot = Tod)
- Grüner Laser ist sicher, Spieler kann ohne Gefahr durchlaufen
	- Color coded Laser mit verschiedenen Eigenschaften
- Dreieck Blöcke verhindern, dass Crate an Wand stecken bleibt
- Auslöser und Empfänger (hier Switch & Tür) visuell verbunden
	- Spieler soll erkennen, was womit verbunden ist
	- wollen später Verbindung visualisieren z.B. durch Kabel
- Spiegel rotiert nicht nur, sondern bewegt sich horizontal, da Pivot unter
eigentlicher Spiegelfläche liegt
	- So bleibt Reflexionspunkt auf Spiegeloberflächer relativ gleich bei Rotation

 ## Level_02
 ### Playthrough
 - schiebe Crate nach rechts auf Button -> Laser aktiviert -> Tür öffnet sich -> Weg aber blockiert
 - schiebe Crate weiter nach rechts, weg vom Button (zwischen Button und Glas)
 - laufe weiter -> 2 zusätzliche Boxen fallen auf
#### Versuch 1: Normal - Grün - Rot
 - schiebe rote Crate nach rechts, springe drauf, springe nach links auf Plattform
 - schiebe grüne Crate nach links runter (Spieler bleibt auf Plattform)
 - schiebe rote Crate nach links
 - (alle Crates sind auf dem Boden | Reihenfolge von links nach rechts: normal Crate - grüne Crate - rote Crate)
 - schiebe rote Crate weiter -> schiebe alle Crates gleichzeitig, bis normale Crate auf Button -> Tür öffnet sich
 - Tür offen, springe runter -> Oh no, roter Laser, Spieler stirbt
 - RESTART
#### Versucht 2: Normal - Rot - Grün
- schiebe rote Crate nach rechts an Steigung
- schiebe rote Crate nach links neben normaler Crate
- laufe weiter nach rechts die Rampe hoch
- springe nach links auf Platform
- schiebe grüne Crate nach links runter
- (alle Crates sind auf dem Boden | Reihenfolge von links nach rechts: normal Crate - rote Crate - grüne Crate)
- schiebe grüne Crate weiter -> schiebe alle Crates gleichzeitig, bis normale Crate auf Button -> Tür öffnet sich
- Springe runter, alles gut
- laufe nach rechts, drücke Button -> Spiegel dreht sich
- Laser aktiviert Switch -> Tür öffnet sich
- laufe nach links durch Tür -> nächstes Level

### Anmerkungen
- Jetzt nicht nur statisches Glas, sondern beweglich durch gläserne Crates
	- Crates blockieren jetzt nicht mehr Laser
	- Multifunktional: Überwinden von Hindernissen (Draufspringen) + Laser Farbe ändern
- Level ist herausfordernder -> Vertikales Denken nötig ggf. RESTART notwendig
- beim 1. Versucht stirbt Spieler -> Todesanimation (Zerspringen in Einzelteile)
- Level Design: Tipp -> 3 Schwarze Panels nebeneinander suggerieren, dass 3 Crates zum Lösen benötigt werden
- Design von größeren Leveln mit Teilzielen (hier: Trapdoor muss geöffnet werden, damit man runter kann)
	- Erstellen von komplexeren, größeren Leveln
- 1. Switch braucht roter Laser zum aktivieren, 2. Switch braucht gründer Laser zum aktivieren
 
## Intro
### Playthrough
- falle zu Boden
- laufe hin und her, siehe Kamera schaut auf Spieler
- bleib stehen, siehe nach unten Scrap Teile + WASD Teile werden geschreddert
- laufe nach rechts, schiebe obere Crate runter
- laufe weiter Treppe hoch
- Tür bereits offen
- springe nach oben, blockiere Laser -> Tür schließt sich kurz
- Laufe durch Tür -> nächstes Level (ALLE LEVEL DURCH)
### Anmerkungen
- Environmental Storytelling: Kamera schaut immer auf Spieler
	-  Spieler unter konstanter Beobachtung
- Environmental Storytelling: Herabfallende Teile (Roboter Schrottteile) werden geschreddert
	- Das wäre normalerweise dem Spieler passiert
- Game Design: Herabfallende WASD Teile als Tipp, wie man sich bewegt
	- "Das versteckte Tutorial"

## Sandbox
### Playthrough
- laufe nach unten rechts links von Crate und Turret
- schaue nach links -> Multi-Reflexionen, Multi-Farbänderungen
- springe auf Crate, dann runter in Schusslinie des Turrets
- lasse dich erschießen
- RESTART (ENDE)
### Anmerkungen
- Experimentelles Sandbox Levelk  zum Testen von Funktionen
- Das meiste hier schon in vorherigen Leveln gezeigt
- *neu* Multi-Rflexionen & Multi-Transmissionen möglich
- *neu* Experimenteller Sentry Turret

# Herausforderungen
- Effizientere Laser -> Object Pools?, EIN Laser mit Farbgradient?
- verbesserter Character Controller
- feintuning bei der Rotation von Spiegeln (schnelle, ungenaue Rotation vs. langsame, genaue Rotation)
- alles selbst Modellieren + Animieren (Aufwand + Stilkonsistenz)

