# RpgStats

Ein Spaßprojekt zur Verbesserung meiner Skills in der Entwicklung von Webanwendungen mit C# und .NET. Die Anwendung ermöglicht das Tracking und die Auswertung von Statuswerten von Charakteren aus RPGs (Rollenspiele).

## Beschreibung
 
### Was macht die App?

Mit RpgStats kannst du:
- Statuswerte deiner Spielcharaktere aus Rollenspielen (RPGs) pro Level protokollieren
- Die Entwicklung eines Charakters im Spielverlauf nachverfolgen
- (Demnächst) Charaktere in übersichtlichen Diagrammen vergleichen
- (Demnächst) Statistiken und Trends in der Charakterentwicklung analysieren

Ideal für RPG-Enthusiasten, die ihre Charaktere detailliert verfolgen und optimieren möchten!

### Welche Technologien werden verwendet?

- **C# und .NET 8.0** - Moderne, typ-sichere Programmiersprache und Framework
- **Blazor** - Für reaktive, client-seitige Webanwendungen mit C#
- **Entity Framework Core** - ORM für die Datenbankinteraktion
- **PostgreSQL** - Robuste, Open-Source-Datenbank
- **Docker** - Containerisierung für einfache Bereitstellung
- **Nginx** - Leistungsstarker Webserver und Reverse-Proxy

### Softwarearchitektur

Das Projekt ist in verschiedene Komponenten aufgeteilt, die jeweils eine spezifische Aufgabe erfüllen:

#### Projekte
- **RpgStats.Domain**
  - Enthält die Kernentitäten und Domänenlogik
  - Definiert die Ausnahmen (Exceptions) des Domänenmodells
  
- **RpgStats.Dto**
  - Data Transfer Objects für die Kommunikation zwischen den Schichten
  - Optimierte Datenstrukturen zur Übertragung über API-Grenzen
  
- **RpgStats.Repo**
  - Enthält den DbContext und Datenbankmigrationen
  - Implementiert die Datenpersistenz
  
- **RpgStats.Services.Abstractions**
  - Definiert die Schnittstellen (Interfaces) für Services
  - Ermöglicht lose Kopplung und erleichtert das Testen
  
- **RpgStats.Services**
  - Implementierung der Service-Layer mit LINQ-Datenbankabfragen
  - Enthält die Geschäftslogik für Datenzugriff und -manipulation
  
- **RpgStats.BizLogic**
  - Enthält Business-Logik, die unabhängig von Persistenz und UI ist
  
- **RpgStats.Faker**
  - Erzeugt Test- und Beispieldaten
  
- **RpgStats.WebApi**
  - RESTful API zur Bereitstellung der Daten
  - Kommunikationsschnittstelle für die UI
  
- **RpgStats.BlazorServer**
  - Webbasierte Benutzeroberfläche

## Installation und Starten

### Voraussetzungen

- Docker und Docker Compose
- Git (zum Klonen des Repositories)

### Schritte zur Installation

1. Das Repository klonen:
   ```
   git clone https://github.com/yourusername/RpgStats.git
   cd RpgStats
   ```

2. Docker-Images erstellen und Container starten:
   ```
   docker compose build
   docker compose up -d
   ```

3. Die Anwendung ist nun über folgende URLs erreichbar:
   - Web-UI: http://localhost:10000
   - API: http://localhost:10000/api

Die vorgefertigte `compose.yaml` richtet alles automatisch ein, inklusive:
- PostgreSQL-Datenbank
- WebAPI
- Blazor Server
- Nginx als Reverse-Proxy

Für detaillierte Informationen zum Erstellen von .NET Docker-Containern: [Microsoft Learn](https://learn.microsoft.com/de-de/dotnet/core/docker/build-container?tabs=windows&pivots=dotnet-8-0)

An einer einfacheren Installation über hub.docker.com wird noch gearbeitet.

## Mitwirken

Beiträge zum Projekt sind willkommen! So kannst du mitwirken:

1. Fork das Repository
2. Erstelle einen Feature-Branch (`git checkout -b feature/amazing-feature`)
3. Committe deine Änderungen (`git commit -m 'Add amazing feature'`)
4. Push zu deinem Branch (`git push origin feature/amazing-feature`)
5. Öffne einen Pull Request

Bitte stelle sicher, dass dein Code den Coding Standards entspricht und füge Tests für neue Funktionalitäten hinzu.

## Lizenz

Dieses Projekt ist unter der MIT-Lizenz lizenziert - siehe die [LICENSE](LICENSE) Datei für Details.

## Kontakt

Bei Fragen oder Anregungen erstelle gerne ein Issue im GitHub-Repository.

---

*Dieses Projekt wurde erstellt, um C# und .NET-Skills zu erweitern und zu demonstrieren. Es dient hauptsächlich zu Lernzwecken.*

