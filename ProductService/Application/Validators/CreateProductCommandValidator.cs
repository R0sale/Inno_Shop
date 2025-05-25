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
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.productForCreation.Description).NotEmpty().WithMessage("Description can't be empty");
            RuleFor(p => p.productForCreation.CreationDate).NotEmpty().WithMessage("CreationDate can't be empty");
            RuleFor(p => p.productForCreation.Price).NotEmpty().WithMessage("Price can't be empty");
            RuleFor(p => p.productForCreation.Name).NotEmpty().WithMessage("Name can't be empty").MinimumLength(5).WithMessage("Length of the namr can't be less than 5").MaximumLength(20).WithMessage("Length of the name can't be more than 20");
            RuleFor(p => p.productForCreation.Accessibility).NotEmpty().WithMessage("Accessibility can't be empty");
        }

        public override ValidationResult Validate(ValidationContext<CreateProductCommand> context)
        {
            return context.InstanceToValidate.productForCreation is null
            ? new ValidationResult(new[] { new ValidationFailure("ProductForCreationDto", "ProductForCreationDto object is null") })
            : base.Validate(context);
        }
    }
}
