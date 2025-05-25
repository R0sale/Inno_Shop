using Application.Contracts;
using Application.Dtos;
using Application.Queries;
using AutoMapper;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands.UserCommands;
using System.Net.Http.Headers;

namespace Application.Handlers.UserControllerHandler
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpClient _client;
        private readonly ICreateTokenService _createTokenService;

        public DeleteUserHandler(UserManager<User> userManager, IHttpClient client, ICreateTokenService createTokenService)
        {
            _userManager = userManager;
            _client = client;
            _createTokenService = createTokenService;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user.UserName is null)
                throw new UserNotFoundException();

            var token = await _createTokenService.CreateTokenAsync(user.UserName);

            var response = await _client.DeleteProducts(token);

            if (!response.IsSuccessStatusCode)
                throw new ServiceUnavailableException();

            await _userManager.DeleteAsync(user);

            return Unit.Value;
        }
    }
}
