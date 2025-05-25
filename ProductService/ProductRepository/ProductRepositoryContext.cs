using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using System.Reflection.Emit;

namespace Repository
{
    public class ProductRepositoryContext : DbContext
    {
        public ProductRepositoryContext(DbContextOptions opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}