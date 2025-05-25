using Application.Commands.AuthenticationCommands;
using Application.Contracts;
using Application.Dtos;
using Application.Queries;
using AutoMapper;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AuthenticationControllerHandler
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterUserHandler(UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var rolesExist = true;

            var user = _mapper.Map<User>(request.UserForRegistration);

            var result = await _userManager.CreateAsync(user, request.UserForRegistration.Password);

            foreach (var role in request.UserForRegistration.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    rolesExist = false;
            }

            if (result.Succeeded && rolesExist)
            {
                await _userManager.AddToRolesAsync(user, request.UserForRegistration.Roles);
            }

            return result;
        }
    }
}
