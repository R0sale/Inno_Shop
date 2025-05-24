using Application.Contracts;
using Application.Dtos;
using Application.Queries.UserQueries;
using Application.RequestFeatures;
using AutoMapper;
using Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserControllerHandler
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = PagedList<User>.ToPagedList(await _userManager.Users.OrderBy(u => u.FirstName).ToListAsync(), request.param.CurrentPage, request.param.PageSize); 

            var usersDTO = _mapper.Map<IEnumerable<UserDTO>>(users).ToList();

            for (int i = 0; i < users.Count; i++)
            {
                var roles = await _userManager.GetRolesAsync(users[i]);

                usersDTO[i].Roles = roles.ToList();
            }

            return usersDTO;
        }
    }
}
