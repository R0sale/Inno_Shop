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
    public class ValidateUserCommandValidator : AbstractValidator<ValidateUserCommand>
    {
        public ValidateUserCommandValidator()
        {
            RuleFor(u => u.UserForAuth.UserName).NotEmpty().MinimumLength(5).MaximumLength(20);
            RuleFor(u => u.UserForAuth.Password).NotEmpty().MinimumLength(5).MaximumLength(10);
        }

        public override ValidationResult Validate(ValidationContext<ValidateUserCommand> context)
        {
            return context.InstanceToValidate.UserForAuth is null
            ? new ValidationResult(new[] { new ValidationFailure("UserForAuthenticationDto", "UserForAuthenticationDto object is null") })
            : base.Validate(context);
        }
    }
}
