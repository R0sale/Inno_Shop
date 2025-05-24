using Application.Dtos;
using Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserQueries
{
    public record GetUserForPartialUpdateQuery(string UserId) : IRequest<(UserForUpdateDTO userForUpd, User user)>;
}
