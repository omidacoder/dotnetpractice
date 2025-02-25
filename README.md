
# Dotnet Practice

This is a simple project to work with different ASP .net core modules.

## Running Backend
To run the ASP backend, you need to download dotnet SDK on your system which is installable using Visual Studio installer (Easiest way to run the project). Open .sln file in visual studio and simply run the project.

### Migrations
Migrations are added to the project and are located in Migrations folder under "DataAccess" layer. The only thing you need to do is running update-database command in Nuget package manager console to run all migrations. Do'nt forget to set your connection string in the PostgresDbContext.cs file.

### Using Dotnet CLI
To run the backend using the command line, use the command below in root folder of the project:
```bash
  dotnet run --project=DotnetPractice.Presentation --urls "https://localhost:7245"
```
In order to run migrations use the command below:
```bash
  dotnet ef database update
```


## Running Frontend
The frontend application is developed using Next Js 15. Make sure you have Node JS version > 20 and navigate to the "DotnetPractice.Frontend" folder and run the following command to install packages:
```bash
  npm install
```
and then run the following commands to run the project:

```bash
  # dev mode
  npm run dev

  # prod mode
  npm run build
  npm run start
```




