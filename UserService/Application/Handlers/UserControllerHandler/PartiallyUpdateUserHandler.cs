using Application.Commands.UserCommands;
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
    public class PartiallyUpdateUserHandler : IRequestHandler<PartiallyUpdateUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public PartiallyUpdateUserHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PartiallyUpdateUserCommand request, CancellationToken cancellationToken)
        {
            _mapper.Map(request.UserForUpd, request.User);

            await _userManager.UpdateAsync(request.User);

            var roles = request.UserForUpd.Roles;

            if (roles != null)
            {
                await _userManager.RemoveFromRolesAsync(request.User, await _userManager.GetRolesAsync(request.User));
                await _userManager.AddToRolesAsync(request.User, roles);
            }

            return Unit.Value;
        }
    }
}
