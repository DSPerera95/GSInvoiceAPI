using GS.Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Invoice.Domain.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<InvoiceDetails>> GetAllInvoicesAsync();
        Task<InvoiceDetails> GetInvoiceByIdAsync(string InvoiceId);
        Task<InvoiceDetails> CreateInvoiceAsync(InvoiceDetails invoiceDetails);
        Task<InvoiceDetails> UpdateInvoiceAsync(string InvoiceId, InvoiceDetails invoiceDetails);
        Task<TransactionResponse> DeleteInvoiceAsync(string InvoiceId);
    }
}
