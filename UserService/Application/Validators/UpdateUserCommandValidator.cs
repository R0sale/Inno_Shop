using Application.Commands.UserCommands;
using Application.Notifications;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.UserForUpd.Email).NotEmpty().WithMessage("Email is a reqired field").EmailAddress();
            RuleFor(c => c.UserForUpd.UserName).NotEmpty()
                .WithMessage("UserName is a reqired field")
                .MaximumLength(20).WithMessage("UserName can't be more than 20 characters")
                .MinimumLength(5).WithMessage("UserName can't be less than 5 characters");
            RuleFor(c => c.UserForUpd.FirstName).NotEmpty()
                .WithMessage("FirstName is a reqired field")
                .MaximumLength(20).WithMessage("FirstName can't be more than 20 characters")
                .MinimumLength(5).WithMessage("FirstName can't be less than 5 characters");
            RuleFor(c => c.UserForUpd.LastName).NotEmpty().WithMessage("LastName is a reqired field")
                .MaximumLength(20).WithMessage("LastName can't be more than 20 characters")
                .MinimumLength(5).WithMessage("LastName can't be less than 5 characters");
            RuleFor(c => c.UserForUpd.Roles).NotEmpty().WithMessage("Roles is a reqired field");
        }

        public override ValidationResult Validate(ValidationContext<UpdateUserCommand> context)
        {
            return context.InstanceToValidate.UserForUpd is null
            ? new ValidationResult(new[] { new ValidationFailure("UserForUpdateDto", "UserForUpdateDto object is null") })
            : base.Validate(context);
        }
    }
}
