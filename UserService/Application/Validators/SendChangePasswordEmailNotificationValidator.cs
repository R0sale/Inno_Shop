using Application.Commands.AuthenticationCommands;
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
    public class SendChangePasswordEmailNotificationValidator : AbstractValidator<SendChangePasswordEmailNotification>
    {
        public SendChangePasswordEmailNotificationValidator()
        {
            RuleFor(c => c.User.Email).NotEmpty().WithMessage("Email is a reqired field").EmailAddress();
            RuleFor(c => c.User.NewPassword).NotEmpty().WithMessage("Password is a reqired field")
                .MinimumLength(5).WithMessage("Password can't be less than 5 characters")
                .MaximumLength(10).WithMessage("Password can't be more than 10 characters");
        }

        public override ValidationResult Validate(ValidationContext<SendChangePasswordEmailNotification> context)
        {
            return context.InstanceToValidate.User is null
            ? new ValidationResult(new[] { new ValidationFailure("ChangePasswordUserDto", "ChangePasswordUserDto object is null") })
            : base.Validate(context);
        }
    }
}
