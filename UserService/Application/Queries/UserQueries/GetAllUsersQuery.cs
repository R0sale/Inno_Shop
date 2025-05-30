﻿using Application.Dtos;
using Application.RequestFeatures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserQueries
{
    public record GetAllUsersQuery(RequestParams param) : IRequest<IEnumerable<UserDTO>>;
}
