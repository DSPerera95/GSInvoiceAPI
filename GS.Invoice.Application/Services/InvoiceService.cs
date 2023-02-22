using GS.Invoice.Application.Interfaces;
using GS.Invoice.Domain.Interfaces;
using GS.Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Invoice.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepo;

        public InvoiceService(IInvoiceRepository invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
        }
        public async Task<IEnumerable<InvoiceDetails>> GetAllInvoicesAsync()
        {
            var result = await _invoiceRepo.GetAllInvoicesAsync();

            return result;
        }

        public async Task<InvoiceDetails> GetInvoiceByIdAsync(string InvoiceId)
        {
            var result = await _invoiceRepo.GetInvoiceByIdAsync(InvoiceId);

            return result;
        }

        public async Task<InvoiceDetails> CreateInvoiceAsync(InvoiceDetails invoiceDetails)
        {
            if (string.IsNullOrEmpty(invoiceDetails.InvoiceId))
            {
                invoiceDetails.InvoiceId = Guid.NewGuid().ToString();
            }

            var result = await _invoiceRepo.CreateInvoiceAsync(invoiceDetails);

            return result;
        }


        public async Task<InvoiceDetails> UpdateInvoiceAsync(string InvoiceId, InvoiceDetails invoiceDetails)
        {
            var result = await _invoiceRepo.UpdateInvoiceAsync(InvoiceId, invoiceDetails);

            return result;
        }

        public async Task<TransactionResponse> DeleteInvoiceAsync(string InvoiceId)
        {
            var result = await _invoiceRepo.DeleteInvoiceAsync(InvoiceId);

            return result;
        }
    }
}
