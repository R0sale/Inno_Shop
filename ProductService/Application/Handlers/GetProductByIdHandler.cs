using Application.Contracts;
using Application.Dtos;
using Application.Queries;
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public record GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProductValidationService _validation;

        public GetProductByIdHandler(IProductRepository repository, IMapper mapper, IProductValidationService validation)
        {
            _repository = repository;
            _mapper = mapper;
            _validation = validation;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellation)
        {
            var product = await _validation.CheckAndFindIfProductExists(request.Id, request.TrackChanges);

            var productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }
    }
}
