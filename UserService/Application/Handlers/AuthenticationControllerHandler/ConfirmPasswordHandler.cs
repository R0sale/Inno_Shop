using Application.Commands.AuthenticationCommands;
using Entities.Exceptions;
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
    class ConfirmPasswordHandler : IRequestHandler<ConfirmPasswordCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;

        public ConfirmPasswordHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(ConfirmPasswordCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new NullParameterException("userId");

            if (string.IsNullOrEmpty(request.EncodedToken))
                throw new NullParameterException("code");

            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                throw new UserNotFoundException();

            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.EncodedToken));

            var result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);

            return result;
        }
    }
}
