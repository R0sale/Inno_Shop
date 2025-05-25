using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public record DeleteUsersProductsCommand(ClaimsPrincipal User) : IRequest<Unit>;
}
