# Azure Queue Trigger to Table Storage (Azure Functions)

This is a sample Azure Functions project that demonstrates how to use a **Queue Trigger**
to write messages into **Azure Table Storage**.

## Functionality
- Listens to messages from an Azure Storage Queue (`myqueue-items`).
- Writes each message as an entity into Table Storage (`QueueMessages` table).

## Running Locally
1. Install [Azurite](https://learn.microsoft.com/azure/storage/common/storage-use-azurite) for local queue and table emulation.
2. Run `func start` from the project folder.
3. Push a message into `myqueue-items` queue:
   ```bash
   az storage queue message add --queue-name myqueue-items --content "Hello from queue" --connection-string "UseDevelopmentStorage=true"
   ```
4. The message will appear in the `QueueMessages` table.

## Deploying to Azure
- Update `AzureWebJobsStorage` in `local.settings.json` with your actual Azure Storage account connection string.
- Deploy with VS, VS Code, or `func azure functionapp publish <APP_NAME>`.

## Requirements
- .NET 7 or 8 SDK
- Azure Functions Core Tools v4+
