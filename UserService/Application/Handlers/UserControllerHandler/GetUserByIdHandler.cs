using Application.Contracts;
using Application.Dtos;
using AutoMapper;
using MediatR;
using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using Application.Queries.UserQueries;

namespace Application.Handlers.UserControllerHandler
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                throw new UserNotFoundException();

            var userDTO = _mapper.Map<UserDTO>(user);

            var roles = await _userManager.GetRolesAsync(user);

            userDTO.Roles = roles.ToList();

            return userDTO;
        }
    }
}
