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
            RuleFor(u => u.User.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.User.NewPassword).NotEmpty().MinimumLength(5).MaximumLength(10);
        }

        public override ValidationResult Validate(ValidationContext<SendChangePasswordEmailNotification> context)
        {
            return context.InstanceToValidate.User is null
            ? new ValidationResult(new[] { new ValidationFailure("ChangePasswordUserDto", "ChangePasswordUserDto object is null") })
            : base.Validate(context);
        }
    }
}
