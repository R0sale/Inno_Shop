using AutoMapper;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Application.Commands.AuthenticationCommands;

namespace Application.Handlers.AuthenticationControllerHandler
{
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;

        public ConfirmEmailHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.EncodedToken));

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            return result;
        }
    }
}
