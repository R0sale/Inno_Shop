using Application.Commands.AuthenticationCommands;
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
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.UserForRegistration.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.UserForRegistration.UserName).NotEmpty().MaximumLength(20).MinimumLength(5);
            RuleFor(u => u.UserForRegistration.FirstName).NotEmpty().MaximumLength(20).MinimumLength(5);
            RuleFor(u => u.UserForRegistration.LastName).NotEmpty().MaximumLength(20).MinimumLength(5);
            RuleFor(u => u.UserForRegistration.Roles).NotEmpty();
            RuleFor(u => u.UserForRegistration.Password).NotEmpty().MaximumLength(10).MinimumLength(5);
        }

        public override ValidationResult Validate(ValidationContext<RegisterUserCommand> context)
        {
            return context.InstanceToValidate.UserForRegistration is null
            ? new ValidationResult(new[] { new ValidationFailure("UserForRegistrationDto", "UserForRegistrationDto object is null") })
            : base.Validate(context);
        }
    }
}
