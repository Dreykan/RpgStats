https://www.freecodecamp.org/news/how-to-write-a-good-readme-file/

# RpgStats
Ein Spaßprojekt um meine Skills in der Entwicklung von Webanwendungen mit C# und .NET zu verbessern.
Mit der Anwendung können Statuswerte von Charakteren aus RPGs getrackt und ausgewertet werden.  

## Stichpunkte was in die ReadMe soll

- [x] Titel
  - [x] Kurze Beschreibung
- [ ] Description / Beschreibung
  - [ ] Was macht die App?
  - [ ] Welche Technologien werden verwendet und warum?
  - [ ] Softwarearchitektur bzw. Beschreibung der Einzelprojekte
- [ ] Installation und Start der App
- [ ] Wie wird die App benutzt?
- [ ] Lizenz der App (MIT)
- [ ] Contribution

# Beschreibung
 
## Was macht die App?
Mit der App können die Statuswerte der Spielcharaktere aus den Rollenspielen (RPG) pro Level aufgeschrieben und 
(demnächst) ausgewertet werden. Somit kann man die Entwicklung eines Charakters nachverfolgen und sehen wie dieser
sich im Laufe des Spiels und Levelingphase entwickelt hat. Gleichzeitig ist es möglich (demnächst) die Charaktere
untereinander zu vergleichen in Form von Diagrammen.

## Welche Technologien werden verwendet?
- C# und .NET
- Blazor
- Entity Framework Core
- Postgresql
- Docker
- Nginx

## Softwarearchitektur
### Projekte
- RpgStats.Domain
  - Hier sind die Entitäten und Exceptions zu finden.
- RpgStats.Dto
  - Hier die Entitäten als Transferobjekte 
- RpgStats.Repo
  - DbContext und Migrationen
- RpgStats.Services.Abstractions
  - Interfaces der Services für die Datenbankabfragen
- RpgStats.Services
  - Die Klassen für die Datenbankabfragen mittels LINQ
- RpgStats.BizLogic
  - Gewisse Business-Logik die weder für die Persistenz noch direkt für die UI relevant sind.
- RpgStats.Faker
  - Zur Speicherung von Fake-Daten.
- RpgStats.WebApi
  - Lauffähige Instanz der Controller als Web-API
- RpgStats.BlazorServer
  - Eine UI als Webapplikation

