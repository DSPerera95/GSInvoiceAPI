using FluentValidation;
using GS.Invoice.Domain.Models;

namespace GS.Invoice.Domain.ModelValidators
{
    public class InvoiceDetailValidator : AbstractValidator<InvoiceDetails>
    {
        public InvoiceDetailValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Please enter a valid date");
            RuleFor(x => x.InvoiceItems).Must(x => x.Count > 0).WithMessage("Please enter invoice items");
            RuleForEach(x => x.InvoiceItems).SetValidator(new InvoiceItemValidator());

        }
    }
}
