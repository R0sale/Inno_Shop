using Application.Commands;
using Application.Contracts;
using AutoMapper;
using Entities.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class DeleteUsersProductsHandler : IRequestHandler<DeleteUsersProductsCommand ,Unit>
    {
        private readonly IProductRepository _repository;

        public DeleteUsersProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteUsersProductsCommand request, CancellationToken cancellation)
        {
            var ownerId = request.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ownerId is null)
                throw new ClaimsPrincipalIdNullException();

            var products = await _repository.GetUsersProducts(ownerId);

            if (products is null)
                throw new ProductsNotFoundException();

            foreach (var product in products)
                _repository.DeleteProductRep(product);

            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}
