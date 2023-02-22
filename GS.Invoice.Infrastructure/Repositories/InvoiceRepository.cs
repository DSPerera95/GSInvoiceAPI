using GS.Invoice.Domain.Interfaces;
using GS.Invoice.Domain.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Invoice.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly Container _container;

        public InvoiceRepository(CosmosClient cosmosDbClient, string databaseId, string containerId)
        {
            _container = cosmosDbClient.GetContainer(databaseId, containerId);
        }

        public async Task<IEnumerable<InvoiceDetails>> GetAllInvoicesAsync()
        {
            List<InvoiceDetails> invoiceList = new();
            
            try
            {
                var selectQuery = "SELECT * FROM c";

                QueryDefinition queryDefinition = new(selectQuery);

                FeedIterator<InvoiceDetails> feedIterator = _container.GetItemQueryIterator<InvoiceDetails>(queryDefinition);


                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<InvoiceDetails> feedResults = await feedIterator.ReadNextAsync();

                    foreach (InvoiceDetails invoice in feedResults)
                    {
                        invoiceList.Add(invoice);
                    }
                }
            }
            catch (CosmosException)
            {
                invoiceList = new();
            }

            return invoiceList;
        }

        public async Task<InvoiceDetails> GetInvoiceByIdAsync(string InvoiceId)
        {
            try
            {
                var response = await _container.ReadItemAsync<InvoiceDetails>(InvoiceId, new PartitionKey(InvoiceId));

                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }

        public async Task<InvoiceDetails> CreateInvoiceAsync(InvoiceDetails invoiceDetails)
        {
            try
            {
                var response = await _container.CreateItemAsync(invoiceDetails, new PartitionKey(invoiceDetails.InvoiceId));

                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }


        public async Task<InvoiceDetails> UpdateInvoiceAsync(string InvoiceId, InvoiceDetails invoiceDetails)
        {
            try
            {
                var response = await _container.UpsertItemAsync(invoiceDetails, new PartitionKey(InvoiceId));

                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }

        public async Task<TransactionResponse> DeleteInvoiceAsync(string InvoiceId)
        {
            try
            {
                var response = await _container.DeleteItemAsync<InvoiceDetails>(InvoiceId, new PartitionKey(InvoiceId));

                return new TransactionResponse() { Success = true, Message = "Invoice Deleted Successfully" };
            }
            catch (CosmosException ex)
            {
                return new TransactionResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
