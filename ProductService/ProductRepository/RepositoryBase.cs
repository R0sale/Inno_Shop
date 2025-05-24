using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public abstract class RepositoryBase : IRepositoryBase
    {
        private readonly ProductRepositoryContext _context;

        public RepositoryBase(ProductRepositoryContext context)
        {
            _context = context;
        }

        public IQueryable<Product> FindAll(bool trackChanges)
        {
            return trackChanges ?
                _context.Set<Product>() :
                _context.Set<Product>().AsNoTracking();
        }

        public IQueryable<Product> FindByCondition(Expression<Func<Product, bool>> expression, bool trackChanges)
        {
            return !trackChanges ? _context.Set<Product>()
                .Where(expression)
                .AsNoTracking() :
                _context.Set<Product>()
                .Where(expression);
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
