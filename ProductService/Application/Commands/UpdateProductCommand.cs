﻿using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public record UpdateProductCommand(Guid Id, ClaimsPrincipal User, ProductForUpdateDTO ProductForUpd, bool TrackChanges) : IRequest<Unit>;
}
