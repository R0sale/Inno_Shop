using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (
                new Product
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Name = "Banana",
                    Description = "Yellow fruit",
                    Price = 18.1m,
                    Accessibility = true,
                    OwnerId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    CreationDate = new DateTime(2024, 5, 12)
                },
                new Product
                {
                    Id = new Guid("3d490a71-94ce-4d12-9494-5248280c2ce3"),
                    Name = "Apple",
                    Description = "Red fruit",
                    Price = 12.1m,
                    Accessibility = true,
                    OwnerId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    CreationDate = new DateTime(2025, 5, 12)
                },
                new Product
                {
                    Id = new Guid("3d420a70-94ce-3d15-9494-5258280c2ce3"),
                    Name = "Orange",
                    Description = "Orange fruit",
                    Price = 15.1m,
                    Accessibility = true,
                    OwnerId = new Guid("3d290a70-94ce-4d15-9292-5248280c2ce3"),
                    CreationDate = new DateTime(2022, 5, 12)
                },
                new Product
                {
                    Id = new Guid("3d440a70-94ce-4d15-9494-5244240c2ce3"),
                    Name = "Pear",
                    Description = "Green fruit",
                    Price = 100.1m,
                    Accessibility = true,
                    OwnerId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    CreationDate = new DateTime(2024, 5, 12)
                }
            );
        }

    }
}

