using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Entities.Exceptions;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Application.Commands.AuthenticationCommands;
using Application.Contracts;

namespace Application.Handlers.AuthenticationControllerHandler
{
    class CreateTokenHandler : IRequestHandler<CreateTokenCommand, string>
    {
        private readonly ICreateTokenService _createTokenService;

        public CreateTokenHandler(ICreateTokenService createTokenService)
        {
            _createTokenService = createTokenService;
        }

        public async Task<string> Handle(CreateTokenCommand request, CancellationToken cancellationToken) => await _createTokenService.CreateTokenAsync(request.UserName);

    }
}
