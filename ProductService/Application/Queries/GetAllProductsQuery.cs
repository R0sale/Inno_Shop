using Application.Dtos;
using Application.RequestFeatures;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public sealed record GetAllProductsQuery(ProductParams Param, bool TrackChanges) : IRequest<IEnumerable<ProductDTO>>;
}
