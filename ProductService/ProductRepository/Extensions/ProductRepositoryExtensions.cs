using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class ProductRepositoryExtensions
    {
        public static IQueryable<Product> FilterProduct(this IQueryable<Product> products, decimal minPrice, decimal maxPrice, DateTime minCreationDate, DateTime maxCreationDate)
        {
            var filteredProduct = products.Where(p => p.Price < maxPrice && 
            p.Price > minPrice && 
            p.CreationDate < maxCreationDate && 
            p.CreationDate > minCreationDate);

            return filteredProduct;
        }

        public static IQueryable<Product> Search(this IQueryable<Product> products, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return products;

            var lowerCaseSearchTerm = searchTerm.ToLower();

            var searchedProducts = products.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));

            return searchedProducts;
        }
    }
}
