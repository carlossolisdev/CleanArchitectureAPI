# Clean Architecture API Demo project

---

To run de project is necesary to have a database. This project is code-first with entity framework. Below is described how to add a migration and update the database.

- **Database is running on Sql Server**

- **To execute the commands you should be placed at root project folder**
---
- **Execute migration(s):**
`dotnet ef database update -s .\CleanAPI.Presentation\CleanAPI.Presentation.csproj -p .\CleanAPI.Infraestructure\CleanAPI.Infraestructure.csproj -c {ContextName}`
---
- **Add a migration:**
`dotnet ef migrations add {ContextName}_{MigrationName} -s .\CleanAPI.Presentation\CleanAPI.Presentation.csproj -p .\CleanAPI.Infraestructure\CleanAPI.Infraestructure.csproj -o Persistence\{ContextName}\Migrations
`
