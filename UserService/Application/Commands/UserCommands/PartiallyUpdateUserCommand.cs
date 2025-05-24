using Application.Dtos;
using Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserCommands
{
    public record PartiallyUpdateUserCommand(User User, UserForUpdateDTO UserForUpd) : IRequest<Unit>;
}
