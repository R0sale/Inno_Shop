using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;
using Application.Commands.AuthenticationCommands;

namespace Application.Handlers.AuthenticationControllerHandler
{
    public class ValidateUserHandler : IRequestHandler<ValidateUserCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public ValidateUserHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ValidateUserCommand request, CancellationToken cancellationToken)
        {
            bool isEmailConfirmed = true;

            var user = await _userManager.FindByNameAsync(request.UserForAuth.UserName);

            if (user is null)
                throw new UserNotFoundException();

            if (!await _userManager.CheckPasswordAsync(user, request.UserForAuth.Password))
                throw new InvalidUserNameOrPasswordException(request.UserForAuth.UserName);

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                isEmailConfirmed = false;
            }

            return isEmailConfirmed;
        }
    }
}
