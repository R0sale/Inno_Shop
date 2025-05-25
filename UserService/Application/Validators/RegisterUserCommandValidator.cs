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
            RuleFor(c => c.UserForRegistration.Email).NotEmpty().WithMessage("Email is a reqired field").EmailAddress();
            RuleFor(c => c.UserForRegistration.UserName).NotEmpty()
                .WithMessage("UserName is a reqired field")
                .MaximumLength(20).WithMessage("UserName can't be more than 20 characters")
                .MinimumLength(5).WithMessage("UserName can't be less than 5 characters");
            RuleFor(c => c.UserForRegistration.FirstName).NotEmpty()
                .WithMessage("FirstName is a reqired field")
                .MaximumLength(20).WithMessage("FirstName can't be more than 20 characters")
                .MinimumLength(5).WithMessage("FirstName can't be less than 5 characters");
            RuleFor(c => c.UserForRegistration.LastName).NotEmpty().WithMessage("LastName is a reqired field")
                .MaximumLength(20).WithMessage("LastName can't be more than 20 characters")
                .MinimumLength(5).WithMessage("LastName can't be less than 5 characters");
            RuleFor(c => c.UserForRegistration.Roles).NotEmpty().WithMessage("Roles is a reqired field");
            RuleFor(c => c.UserForRegistration.Password).NotEmpty().WithMessage("Password is a reqired field")
                .MinimumLength(5).WithMessage("Password can't be less than 5 characters")
                .MaximumLength(10).WithMessage("Password can't be more than 10 characters");
        }

        public override ValidationResult Validate(ValidationContext<RegisterUserCommand> context)
        {
            return context.InstanceToValidate.UserForRegistration is null
            ? new ValidationResult(new[] { new ValidationFailure("UserForRegistrationDto", "UserForRegistrationDto object is null") })
            : base.Validate(context);
        }
    }
}
