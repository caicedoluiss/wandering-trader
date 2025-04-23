# Wandering Tader

Web solution for recording daily sales, cost and profits of one personal business.

## How to execute it locally

### WebAPI

#### Rquirements

-   .NET 8
-   Azure Cosmos Db

#### Run/Debug

Set up the CosmosDb required configuration `./server/WanderingTrader.WebAPI/appsetings.json` directory, with this specific information (Do not track changes in this file, run `git update-index --assume-uncganged ./server/WanderingTrader.WebAPI/appsettings.json` untrack changes, use `appsettings.example.json` instead to leave any configuration reference):

```json
"CosmosConfig": {
    "Endpoint": "https://azure.endpoint/",
    "Token": "Token Access Key from resource settings",
    "DbName": "Db Name"
}
```

From vscode, open a terminal in the WebAPI project and run `dotnet run -- migrations`.
This will prepare and seed the db (No actual migrations executed as this is a document based Non-Relation db).

Then you can run the project as prefered.

-   By using the **Run and Debug** vscode option over _.Net Core Launch (web) (WanderingTrader.WebAPI)_
-   running `dotnet run`
-   running `dotnet watch`

The Swagger web interface should be displayed.

### Web Client

#### Requirements

-   Node 20.18.0.

#### Run/Debug

Create an _.env.local_ file inside `./client/WebClient/` directory and set all the variables found in _.env_ file.

Install all the node dependencies by running `npm i`.

Execute the dev script by running `npm run dev` or `npm run dev -- --host`

## Documentation

#### .NET environment

This solution has a custom environment configuration for dotnet. the `./server/WanderingTrader.Application/AppEnvironment.cs` file has the Enum definition with all the values:

-   Debug
-   Local
-   Development
-   Staging
-   Production

Make sure to set it up properly by setting the **ASPNETCORE_ENVIRONMENT** env variable.
