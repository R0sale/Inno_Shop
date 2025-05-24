using Application.Dtos;
using Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public record GetProductForPartialUpdateQuery(Guid Id, ClaimsPrincipal User, bool TrackChanges) : IRequest<(ProductForUpdateDTO productForUpd, Product product)>;
}
