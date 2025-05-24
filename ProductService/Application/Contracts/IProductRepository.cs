using Entities.Models;
using Application.RequestFeatures;
using System.Linq.Expressions;

namespace Application.Contracts
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetAllProducts(ProductParams productParams, bool trackChanges);
        Task<Product> GetProduct(Guid id, bool trackChanges);
        void CreateProductRep(Product product);
        void DeleteProductRep(Product product);
        Task SaveAsync();
    }
}
