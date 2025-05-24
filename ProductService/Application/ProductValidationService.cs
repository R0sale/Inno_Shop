using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;
using Entities.Models;

namespace Application
{
    public class ProductValidationService : IProductValidationService
    {
        private readonly IProductRepository _repository;

        public ProductValidationService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> CheckAndFindIfProductExists(Guid id, bool trackChanges)
        {
            var product = await _repository.GetProduct(id, trackChanges);

            if (product is null)
                throw new ProductNotFoundException(id);

            return product;
        }
    }
}
