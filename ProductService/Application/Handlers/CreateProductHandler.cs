using Application.Contracts;
using Application.Dtos;
using Application.Queries;
using Application.Commands;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Exceptions;
using System.Security.Claims;

namespace Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDTO>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(CreateProductCommand request, CancellationToken cancellation)
        {
            var product = _mapper.Map<Product>(request.productForCreation);

            var ownerId = request.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ownerId is null)
                throw new ClaimsPrincipalIdNullException();

            product.OwnerId = new Guid(ownerId);

            _repository.CreateProductRep(product);
            await _repository.SaveAsync();

            var productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }
    }
}
