using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public record DeleteProductCommand(Guid Id, ClaimsPrincipal User, bool TrackChanges) : IRequest<Unit>;
}
