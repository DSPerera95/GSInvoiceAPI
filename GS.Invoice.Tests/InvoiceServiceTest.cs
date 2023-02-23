using GS.Invoice.Application.Interfaces;
using GS.Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                            Quantity = 2,
                            UnitPrice = 1000,
                            Amount = 2000,
                            LineAmount = 2000
                        },
                        new InvoiceItem()
                        {
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
                            Quantity = 2,
                            UnitPrice = 2000,
                            Amount = 4000,
                            LineAmount = 4000
                        },
                        new InvoiceItem()
                        {
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
            
            var invoice = invoiceList.FirstOrDefault(s => s.InvoiceId == invoiceDetails.InvoiceId);

            if (invoice is null)
            {
                return null;
            }

            return invoice;
        }

        public async Task<TransactionResponse> DeleteInvoiceAsync(string InvoiceId)
        {
            await Task.Delay(500);

            var invoice = invoiceList.FirstOrDefault(s => s.InvoiceId == InvoiceId);

            if (invoice is null)
            {
                return new TransactionResponse()
                {
                    Success = false,
                    Message = "Cannot find invoice with the provided inovice id"
                };
            }

            invoiceList.Remove(invoice);

            return new TransactionResponse()
            {
                Success = false,
                Message = "Invoice deleted successfully!"
            };
        }

        public async Task<IEnumerable<InvoiceDetails>> GetAllInvoicesAsync()
        {
            await Task.Delay(500);
            
            return invoiceList;
        }

        public async Task<InvoiceDetails> GetInvoiceByIdAsync(string InvoiceId)
        {
            
            var invoice = invoiceList.FirstOrDefault(s => s.InvoiceId == InvoiceId);

            if (invoice is null)
            {
                return null;
            }

            await Task.Delay(500);
            
            return invoice;
        }

        public async Task<InvoiceDetails> UpdateInvoiceAsync(string InvoiceId, InvoiceDetails invoiceDetails)
        {
            var existingInvoice = invoiceList.FirstOrDefault(s => s.InvoiceId == InvoiceId);

            if (existingInvoice is null)
            {
                return null;
            }

            existingInvoice.InvoiceId = InvoiceId;
            existingInvoice.Description = invoiceDetails.Description;
            existingInvoice.Date = invoiceDetails.Date;
            existingInvoice.TotalAmount = invoiceDetails.TotalAmount;
            existingInvoice.InvoiceItems = invoiceDetails.InvoiceItems;

            await Task.Delay(500);

            return existingInvoice;
        }
    }
}
