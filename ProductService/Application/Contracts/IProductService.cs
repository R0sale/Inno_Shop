using Entities.Models;
using Application.Dtos;
using Application.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync(ProductParams productParams, bool trackChanges);
        Task<ProductDTO> GetProductAsync(Guid id, bool trackChanges);
        Task<ProductDTO> CreateProduct(ProductForCreationDTO productForCreation);
        Task DeleteProduct(Guid id, ClaimsPrincipal User, bool trackChanges);
        Task UpdateProduct(Guid id, ProductForUpdateDTO productForUpd, ClaimsPrincipal User, bool trackChanges);
        Task<(ProductForUpdateDTO productForUpd, Product productEntity)> GetProductForPatialUpdate(Guid id, ClaimsPrincipal User, bool trackChanges);
        Task SaveChangesForPatrialUpdate(ProductForUpdateDTO productForUpd, Product product);
    }
}
