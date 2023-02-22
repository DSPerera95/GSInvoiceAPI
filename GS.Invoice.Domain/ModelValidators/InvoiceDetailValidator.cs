using FluentValidation;
using GS.Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Invoice.Domain.ModelValidators
{
    public class InvoiceDetailValidator : AbstractValidator<InvoiceDetails>
    {
        public InvoiceDetailValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Please enter a valid date");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("Total amount should be greater than 0");
            RuleForEach(x => x.InvoiceItems).SetValidator(new InvoiceItemValidator());

        }
    }
}
