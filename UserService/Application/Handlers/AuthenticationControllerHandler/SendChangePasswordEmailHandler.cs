using Application.Commands.AuthenticationCommands;
using Application.Contracts;
using Application.Notifications;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AuthenticationControllerHandler
{
    class SendChangePasswordEmailHandler : INotificationHandler<SendChangePasswordEmailNotification>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public SendChangePasswordEmailHandler(UserManager<User> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task Handle(SendChangePasswordEmailNotification request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.User.Email);

            if (user == null)
                throw new UserNotFoundException();

            if (!await _userManager.IsEmailConfirmedAsync(user))
                throw new NotConfirmedEmailException($"Your email {user.Email} is not confirmed");

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUri = _emailService.GenerateRestoreLink(user.Id, code, request.User.NewPassword);

            await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Hello {user.UserName}, to confirm your email please follow the link <a href=\"{callbackUri}\">link</a>");

            await Task.CompletedTask;
        }
    }
}
