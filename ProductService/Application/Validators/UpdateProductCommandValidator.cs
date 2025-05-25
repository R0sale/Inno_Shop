using Application.Commands;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.ProductForUpd.Description).NotEmpty().WithMessage("Description can't be empty");
            RuleFor(p => p.ProductForUpd.CreationDate).NotEmpty().WithMessage("CreationDate can't be empty");
            RuleFor(p => p.ProductForUpd.OwnerId).NotEmpty().WithMessage("OwnerId can't be empty");
            RuleFor(p => p.ProductForUpd.Price).NotEmpty().WithMessage("Price can't be empty");
            RuleFor(p => p.ProductForUpd.Name).NotEmpty().WithMessage("Name can't be empty").MinimumLength(5).WithMessage("Length of the namr can't be less than 5").MaximumLength(20).WithMessage("Length of the name can't be more than 20");
            RuleFor(p => p.ProductForUpd.Accessibility).NotEmpty().WithMessage("Accessibility can't be empty");
        }

        public override ValidationResult Validate(ValidationContext<UpdateProductCommand> context)
        {
            return context.InstanceToValidate.ProductForUpd is null
            ? new ValidationResult(new[] { new ValidationFailure("ProductForUpdateDto", "ProductForUpdateDto object is null") })
            : base.Validate(context);
        }
    }
}
