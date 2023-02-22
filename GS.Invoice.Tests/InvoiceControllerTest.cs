using GS.Invoice.API.Controllers;
using GS.Invoice.Application.Interfaces;
using GS.Invoice.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GS.Invoice.Tests
{
    public class InvoiceControllerTest
    {
        private readonly InvoiceController _controller;
        private readonly IInvoiceService _service;
        public InvoiceControllerTest()
        {
            _service = new InvoiceServiceTest();
            _controller = new InvoiceController(_service);
        }

        [Fact]
        public async Task Add_ValidInvoice_Returns_OkResult()
        {
            // Arrange
            var validInvoice = new InvoiceDetails()
            {
                InvoiceId = Guid.NewGuid().ToString(),
                Description = "Test Description 3",
                TotalAmount = 100,
                Date = DateTime.Now,
                InvoiceItems = new List<InvoiceItem>()
                {
                    new InvoiceItem()
                    {
                        ItemId = 1,
                        Amount = 100,
                        Quantity = 1,
                        UnitPrice = 100,
                        LineAmount = 100
                    }
                }
            };

            // Act
            var actionResult = await _controller.CreateInvoice(validInvoice);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var result = Object.Equals(validInvoice, actionResult);
            Assert.True(result);
        }

        [Fact]
        public async Task Add_Invoice_With_TotalAmountZero_Returns_BadRequest()
        {
            // Arrange
            var totalAamountZeroInvoice  = new InvoiceDetails()
            {
                InvoiceId = Guid.NewGuid().ToString(),
                Description = "Test Description",
                TotalAmount = 0,
                Date = DateTime.Now,
                InvoiceItems = new List<InvoiceItem>()
                {
                    new InvoiceItem()
                    {
                        ItemId = 1,
                        Amount = 100,
                        Quantity = 1,
                        UnitPrice = 100,
                        LineAmount = 100
                    }
                }
            };

            _controller.ModelState.AddModelError("TotalAmount", "Total amount should be greater than 0");
           
            // Act
            var actionResult = await _controller.CreateInvoice(totalAamountZeroInvoice);
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
    }
}
