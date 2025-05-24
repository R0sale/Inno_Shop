using Application.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ProductService.ActionFilters;
using Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application;

namespace ProductService.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureProductRepository(this IServiceCollection service)
        {
            service.AddScoped<IProductRepository, Repository.ProductRepository>();
        }

        public static void ConfigureSqlContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ProductRepositoryContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("sqlConnection"));
            });
        }

        public static void ConfigureValidationFilter(this IServiceCollection service) => service.AddScoped<ValidationFilterAttribute>();

        public static void ConfigureHttpClient(this IServiceCollection service)
        {
            service.AddScoped<HttpClient>();
            service.AddScoped<IHttpClient, ProductClient>();
        }

        public static void ConfigureMediatR(this IServiceCollection services) => services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly));

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtSettings");
            var key = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings["validIssuer"],
                        ValidAudience = jwtSettings["validAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }

        public static void ConfigureValidationService(this IServiceCollection services)
        {
            services.AddScoped<IProductValidationService, ProductValidationService>();
        }
    }
}
