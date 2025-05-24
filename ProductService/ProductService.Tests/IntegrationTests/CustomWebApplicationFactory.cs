using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Repository;
using Entities.Models;

namespace ProductService.Tests.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ProductRepositoryContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ProductRepositoryContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ProductRepositoryContext>();
                    db.Database.EnsureCreated();

                    db.Products.Add
                    (
                        new Product
                        {
                            Id = new Guid("3b6e3995-046a-4c51-a65a-a5d419e23783"),
                            Name = "Chair",
                            Description = "wooden chair",
                            Accessibility = true,
                            Price = 120m,
                            CreationDate = new DateTime(2024, 10, 2),
                            OwnerId = new Guid("27362914-3eb1-401a-a16a-fbbb9e99857f")
                        }
                    );
                    db.Products.Add
                    (
                        new Product
                        {
                            Id = new Guid("2C13FDB2-83A5-4F1A-1829-08DD95525D58"),
                            OwnerId = new Guid("27362914-3eb1-401a-a16a-fbbb9e99857f"),
                            Name = "Orange",
                            Description = "orange",
                            Accessibility = true,
                            Price = 20m,
                            CreationDate = new DateTime(2024, 10, 2)
                        }
                    );
                    db.SaveChanges();
                }
            });
        }
    }
}
