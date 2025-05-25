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
            RuleFor(c => c.UserForAuth.UserName).NotEmpty()
                .WithMessage("UserName is a reqired field")
                .MaximumLength(20).WithMessage("UserName can't be more than 20 characters")
                .MinimumLength(5).WithMessage("UserName can't be less than 5 characters");
            RuleFor(c => c.UserForAuth.Password).NotEmpty().WithMessage("Password is a reqired field")
                .MinimumLength(5).WithMessage("Password can't be less than 5 characters")
                .MaximumLength(10).WithMessage("Password can't be more than 10 characters");
        }

        public override ValidationResult Validate(ValidationContext<ValidateUserCommand> context)
        {
            return context.InstanceToValidate.UserForAuth is null
            ? new ValidationResult(new[] { new ValidationFailure("UserForAuthenticationDto", "UserForAuthenticationDto object is null") })
            : base.Validate(context);
        }
    }
}
