using Application.Contracts;
using Application.Dtos;
using Application.Queries;
using Entities.Models;
using Application.RequestFeatures;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellation)
        {
            var products = await _repository.GetAllProducts(request.Param ,request.TrackChanges);

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return productsDTO;
        }
    }
}
