using Application.Commands.UserCommands;
using Application.Dtos;
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserControllerHandler
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UpdateUserHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserForUpd.UserName);

            if (user is null)
                throw new UserNotFoundException();

            _mapper.Map(request.UserForUpd, user);

            await _userManager.UpdateAsync(user);

            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

            await _userManager.AddToRolesAsync(user, request.UserForUpd.Roles);

            return Unit.Value;
        }
    }
}
