using Application.Contracts;
using Application.Dtos;
using Application.Queries.UserQueries;
using AutoMapper;
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
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetUserByEmailHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            var userDTO = _mapper.Map<UserDTO>(user);

            var roles = await _userManager.GetRolesAsync(user);

            userDTO.Roles = roles.ToList();

            return userDTO;
        }
    }
}
