using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AuthenticationCommands
{
    public record RegisterUserCommand(UserForRegistrationDTO userForRegistration) : IRequest<IdentityResult>;
}
