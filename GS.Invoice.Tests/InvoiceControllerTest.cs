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
                TotalAmount = 300,
                Date = DateTime.Now,
                InvoiceItems = new List<InvoiceItem>()
                {
                    new InvoiceItem()
                    {
                        ItemId = 1,
                        Amount = 100,
                        Quantity = 2,
                        UnitPrice = 50,
                        LineAmount = 100
                    },
                    new InvoiceItem()
                    {
                        ItemId = 2,
                        Amount = 200,
                        Quantity = 1,
                        UnitPrice = 200,
                        LineAmount = 200
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
                TotalAmount = 300,
                Date = DateTime.Now,
                InvoiceItems = new List<InvoiceItem>()
                {
                    new InvoiceItem()
                    {
                        ItemId = 1,
                        Amount = 5000,
                        Quantity = 4,
                        UnitPrice = 1250,
                        LineAmount = 5000
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


        //Unit test for total amount validation error
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


        //Unit test for invoice items empty validation error
        [Fact]
        public async Task Update_Invoice_With_InvoiceItemsAsEmpty_Returns_BadRequest()
        {
            // Arrange
            var invoiceItemsEmptyInvoice = new InvoiceDetails()
            {
                InvoiceId = Guid.NewGuid().ToString(),
                Description = "Test Description",
                TotalAmount = 400,
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
