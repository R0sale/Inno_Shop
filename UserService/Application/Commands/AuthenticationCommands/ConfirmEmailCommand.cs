using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AuthenticationCommands
{
    public record ConfirmEmailCommand(string UserId, string EncodedToken) : IRequest<IdentityResult>;
}
