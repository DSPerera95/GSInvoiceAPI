using GS.Invoice.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace GS.Invoice.Infrastructure
{
    public static class DependencyResolver
    {
        public static async Task<InvoiceRepository> InitializeInvoiceRepositoryInstanceAsync(IConfigurationSection configurationSection)
        {
            var databaseId = configurationSection["DatabaseId"];
            var containerId = configurationSection["ContainerId"];
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];
            
            var client = new CosmosClient(account, key);
            
            //DB Creation
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseId);

            //Container Creation
            await database.Database.CreateContainerIfNotExistsAsync(containerId, "/id");
            
            //Initialize Repository
            var invoiceRepository = new InvoiceRepository(client, databaseId, containerId);
            
            return invoiceRepository;
        }
    }
}
