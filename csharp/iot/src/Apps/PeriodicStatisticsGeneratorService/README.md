# Periodic Statistics Generator Service

> This service generate periodic statistics in database. The project is a
> dotnet core asp version 7.x. The project includs a small API to stop the 
> program.

#### The resolution of the timer used in this code is 1 second.

### Generated statistics
- Minute statistics (table minute)
- Hour statistics (table hour)

### How to run the application
```sh
cd src/apps/PeriodicStatisticsGeneratorService
dotnet run
```

### How to check status of application

| Operation  | Command  | 
|------------|----------|
| Get status | curl --location --request GET 'https://localhost:5001/background' |
| Quit       | curl --location --request PATCH 'https://localhost:5001/background' --header 'Content-Type: application/json' --data-raw '{"IsEnabled": false}'      | 

Inspired by this article: https://medium.com/medialesson/run-and-manage-periodic-background-tasks-in-asp-net-core-6-with-c-578a31f4b7a3
