using GS.Invoice.Application.Interfaces;
using GS.Invoice.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GS.Invoice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        // Get all the created invoices
        [HttpGet]
        [Route("GetAllInvoices")]
        public async Task<IActionResult> GetAllInvoices()
        {
            var response = await _invoiceService.GetAllInvoicesAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // Get specific invoice based on the invoice id
        [HttpGet]
        [Route("GetInvoiceById")]
        public async Task<IActionResult> GetInvoiceById([FromQuery] string InvoiceId)
        {
            var response = await _invoiceService.GetInvoiceByIdAsync(InvoiceId);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // Create a new invoice
        [HttpPost]
        [Route("CreateInvoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDetails invoiceDetails)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _invoiceService.CreateInvoiceAsync(invoiceDetails);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        // Update an exisitng invoice
        [HttpPut]
        [Route("UpdateInvoice")]
        public async Task<IActionResult> UpdateInvoice([FromBody] InvoiceDetails invoiceDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _invoiceService.UpdateInvoiceAsync(invoiceDetails.InvoiceId, invoiceDetails);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        // Delete an exisitng invoice
        [HttpDelete]
        [Route("DeleteInvoice")]
        public async Task<IActionResult> DeleteInvoice([FromQuery] string InvoiceId)
        {

            var response = await _invoiceService.DeleteInvoiceAsync(InvoiceId);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}
