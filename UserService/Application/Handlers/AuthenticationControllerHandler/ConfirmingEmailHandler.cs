using Application.Contracts;
using Application.Notifications;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AuthenticationControllerHandler
{
    public class ConfirmingEmailHandler : INotificationHandler<UserConfirmingEmailNotification>
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public ConfirmingEmailHandler(IEmailService emailService, UserManager<User> userManager)
        {
            _emailService = emailService;
            _userManager = userManager;
        }

        public async Task Handle(UserConfirmingEmailNotification notification, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(notification.userId);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUri = _emailService.GenerateEmailLink(user.Id, code);

            await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Hello {user.UserName}, to confirm your email please follow the link <a href=\"{callbackUri}\">link</a>");

            await Task.CompletedTask;
        }
    }
}
