using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IRepositoryBase
    {
        IQueryable<Product> FindAll(bool trackChanges);
        IQueryable<Product> FindByCondition(Expression<Func<Product, bool>> expression, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        Task SaveAsync();
    }
}
