using Application.Dtos;
using AutoMapper;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.Queries.UserQueries;

namespace Application.Handlers.UserControllerHandler
{
    class GetUserForPartialUpdateHandler : IRequestHandler<GetUserForPartialUpdateQuery, (UserForUpdateDTO userForUppd, User user)>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetUserForPartialUpdateHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<(UserForUpdateDTO userForUppd, User user)> Handle(GetUserForPartialUpdateQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                throw new UserNotFoundException();

            var userForUpd = _mapper.Map<UserForUpdateDTO>(user);

            var roles = await _userManager.GetRolesAsync(user);

            if (roles is not null)
                userForUpd.Roles = roles;

            return (userForUpd, user);
        }
    }
}
