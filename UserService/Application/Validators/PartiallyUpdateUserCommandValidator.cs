using Application.Commands.UserCommands;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class PartiallyUpdateUserCommandValidator : AbstractValidator<PartiallyUpdateUserCommand>
    {
        public PartiallyUpdateUserCommandValidator()
        {
            RuleFor(c => c.UserForUpd.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.UserForUpd.UserName).NotEmpty().MaximumLength(20).MinimumLength(5);
            RuleFor(c => c.UserForUpd.FirstName).NotEmpty().MaximumLength(20).MinimumLength(5);
            RuleFor(c => c.UserForUpd.LastName).NotEmpty().MaximumLength(20).MinimumLength(5);
            RuleFor(c => c.UserForUpd.Roles).NotEmpty();
        }

        public override ValidationResult Validate(ValidationContext<PartiallyUpdateUserCommand> context)
        {
            return context.InstanceToValidate.UserForUpd is null
            ? new ValidationResult(new[] { new ValidationFailure("UserForUpdateDto", "UserForUpdateDto object is null") })
            : base.Validate(context);
        }

    }
}

