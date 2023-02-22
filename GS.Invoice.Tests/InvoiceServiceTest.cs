using GS.Invoice.Application.Interfaces;
using GS.Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Invoice.Tests
{
    public class InvoiceServiceTest : IInvoiceService
    {
        private readonly List<InvoiceDetails> invoiceList;

        public InvoiceServiceTest()
        {
            invoiceList = new List<InvoiceDetails>()
            {
                new InvoiceDetails() 
                {
                    InvoiceId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200").ToString(),
                    Date = DateTime.Now,
                    Description = "Test Description 1",
                    TotalAmount = 3000,
                    InvoiceItems = new List<InvoiceItem>()
                    {
                        new InvoiceItem()
                        {
                            ItemId = 1,
                            Quantity = 2,
                            UnitPrice = 1000,
                            Amount = 2000,
                            LineAmount = 2000
                        },
                        new InvoiceItem()
                        {
                            ItemId = 2,
                            Quantity = 1,
                            UnitPrice = 1000,
                            Amount = 1000,
                            LineAmount = 1000
                        },
                    }
                },
                new InvoiceDetails()
                {
                    InvoiceId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c300").ToString(),
                    Date = DateTime.Now,
                    Description = "Test Description 2",
                    TotalAmount = 6000,
                    InvoiceItems = new List<InvoiceItem>()
                    {
                        new InvoiceItem()
                        {
                            ItemId = 1,
                            Quantity = 2,
                            UnitPrice = 2000,
                            Amount = 4000,
                            LineAmount = 4000
                        },
                        new InvoiceItem()
                        {
                            ItemId = 2,
                            Quantity = 1,
                            UnitPrice = 2000,
                            Amount = 2000,
                            LineAmount = 2000
                        },
                    }
                },
            };
        }

        public async Task<InvoiceDetails> CreateInvoiceAsync(InvoiceDetails invoiceDetails)
        {
            if (string.IsNullOrEmpty(invoiceDetails.InvoiceId))
            {
                invoiceDetails.InvoiceId = new Guid().ToString();

            }

            invoiceList.Add(invoiceDetails);

            await Task.Delay(500);
            
            var invoiceDetailList = invoiceList.FirstOrDefault(s => s.InvoiceId == invoiceDetails.InvoiceId);

            if (invoiceDetailList != null)
            {
                return invoiceDetailList;
            }

            return new InvoiceDetails();
        }

        public Task<TransactionResponse> DeleteInvoiceAsync(string InvoiceId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvoiceDetails>> GetAllInvoicesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceDetails> GetInvoiceByIdAsync(string InvoiceId)
        {
            throw new NotImplementedException();
        }

        public async Task<InvoiceDetails> UpdateInvoiceAsync(string InvoiceId, InvoiceDetails invoiceDetails)
        {
            throw new NotImplementedException();
        }
    }
}
