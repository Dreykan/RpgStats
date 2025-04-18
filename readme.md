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

---

# RpgStats [English Version]

A fun project to improve my skills in web application development using C# and .NET. The application allows tracking and analyzing status values of characters from RPGs (Role Playing Games).

## Description
 
### What does the app do?

With RpgStats you can:
- Record status values of your RPG characters per level
- Track the development of a character throughout gameplay
- (Coming soon) Compare characters in clear, visual diagrams
- (Coming soon) Analyze statistics and trends in character development

Ideal for RPG enthusiasts who want to track and optimize their characters in detail!

### Technologies used

- **C# and .NET 8.0** - Modern, type-safe programming language and framework
- **Blazor** - For reactive, client-side web applications using C#
- **Entity Framework Core** - ORM for database interaction
- **PostgreSQL** - Robust, open-source database
- **Docker** - Containerization for easy deployment
- **Nginx** - Powerful web server and reverse proxy

### Software Architecture

The project is divided into various components, each fulfilling a specific task:

#### Projects
- **RpgStats.Domain**
  - Contains core entities and domain logic
  - Defines domain model exceptions
  
- **RpgStats.Dto**
  - Data Transfer Objects for communication between layers
  - Optimized data structures for transfer across API boundaries
  
- **RpgStats.Repo**
  - Contains DbContext and database migrations
  - Implements data persistence
  
- **RpgStats.Services.Abstractions**
  - Defines interfaces for services
  - Enables loose coupling and facilitates testing
  
- **RpgStats.Services**
  - Implementation of the service layer with LINQ database queries
  - Contains business logic for data access and manipulation
  
- **RpgStats.BizLogic**
  - Contains business logic independent of persistence and UI
  
- **RpgStats.Faker**
  - Generates test and sample data
  
- **RpgStats.WebApi**
  - RESTful API for providing data
  - Communication interface for the UI
  
- **RpgStats.BlazorServer**
  - Web-based user interface

## Installation and Launch

### Prerequisites

- Docker and Docker Compose
- Git (for cloning the repository)

### Installation Steps

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/RpgStats.git
   cd RpgStats
   ```

2. Build Docker images and start containers:
   ```
   docker compose build
   docker compose up -d
   ```

3. The application is now accessible via the following URLs:
   - Web UI: http://localhost:10000
   - API: http://localhost:10000/api

The predefined `compose.yaml` sets up everything automatically, including:
- PostgreSQL database
- WebAPI
- Blazor Server
- Nginx as reverse proxy

For detailed information on creating .NET Docker containers: [Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=windows&pivots=dotnet-8-0)

A simpler installation via hub.docker.com is still in progress.

## Contributing

Contributions to the project are welcome! Here's how you can contribute:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to your branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

Please ensure your code adheres to coding standards and add tests for new features.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

For questions or suggestions, please create an issue in the GitHub repository.

---

*This project was created to expand and demonstrate C# and .NET skills. It primarily serves educational purposes.*

