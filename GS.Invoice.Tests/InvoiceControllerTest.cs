using GS.Invoice.API.Controllers;
using GS.Invoice.Application.Interfaces;
using GS.Invoice.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        //Unit test for successful invoice creation
        [Fact]
        public async Task Add_ValidInvoice_Returns_OkResult()
        {
            // Arrange
            var validInvoice = new InvoiceDetails()
            {
                InvoiceId = Guid.NewGuid().ToString(),
                Description = "Test Description 3",
                Date = DateTime.Now,
                InvoiceItems = new List<InvoiceItem>()
                {
                    new InvoiceItem()
                    {
                        Quantity = 2,
                        UnitPrice = 50
                    },
                    new InvoiceItem()
                    {
                        Quantity = 1,
                        UnitPrice = 200
                    }
                }
            };

            // Act
            var actionResult = await _controller.CreateInvoice(validInvoice);

            var okObjectResult = actionResult as OkObjectResult;
            var result = Object.Equals(validInvoice, okObjectResult.Value);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.True(result);
        }


        //Unit test for successful invoice update
        [Fact]
        public async Task Update_ValidInvoice_Returns_OkResult()
        {
            // Arrange
            var validUpdatedInvoice = new InvoiceDetails()
            {
                InvoiceId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200").ToString(),
                Description = "New Test Description",
                Date = DateTime.Now,
                InvoiceItems = new List<InvoiceItem>()
                {
                    new InvoiceItem()
                    {
                        Quantity = 4,
                        UnitPrice = 1250
                    }
                }
            };

            // Act
            var actionResult = await _controller.UpdateInvoice(validUpdatedInvoice);

            var okObjectResult = actionResult as OkObjectResult;
            var result = Object.Equals(validUpdatedInvoice.ToString(), okObjectResult.Value.ToString());

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.True(result);
        }


        //Unit test for date validation error
        [Fact]
        public async Task Add_Invoice_Without_Date_Returns_BadRequest()
        {
            // Arrange
            var invalidDateInvoice  = new InvoiceDetails()
            {
                InvoiceId = Guid.NewGuid().ToString(),
                Description = "Test Description",
                InvoiceItems = new List<InvoiceItem>()
                {
                    new InvoiceItem()
                    {
                        Quantity = 1,
                        UnitPrice = 100
                    }
                }
            };

            _controller.ModelState.AddModelError("Date", "Please enter a valid date");
           
            // Act
            var actionResult = await _controller.CreateInvoice(invalidDateInvoice);
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }


        //Unit test for invoice items empty validation error
        [Fact]
        public async Task Update_Invoice_With_InvoiceItemsAsEmpty_Returns_BadRequest()
        {
            // Arrange
            var invoiceItemsEmptyInvoice = new InvoiceDetails()
            {
                InvoiceId = Guid.NewGuid().ToString(),
                Description = "Test Description",
                Date = DateTime.Now,
                InvoiceItems = new List<InvoiceItem>()
            };

            _controller.ModelState.AddModelError("InvoiceItems", "Please enter invoice items");

            // Act
            var actionResult = await _controller.UpdateInvoice(invoiceItemsEmptyInvoice);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

    }
}
