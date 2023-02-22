using FluentValidation;
using GS.Invoice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Invoice.Domain.ModelValidators
{
    public class InvoiceItemValidator : AbstractValidator<InvoiceItem>
    {
        public InvoiceItemValidator()
        {
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Unit price should be greater than 0");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity should be greater than 0");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount should be greater than 0");
            RuleFor(x => x.LineAmount).GreaterThan(0).WithMessage("Line amount should be greater than 0");
        }
    }
}
