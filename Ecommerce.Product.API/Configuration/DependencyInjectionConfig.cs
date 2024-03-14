using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Product.API.Data;
using Ecommerce.Product.API.Data.Repository;
using Ecommerce.Product.API.Models;

namespace Ecommerce.Product.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalogoContext>();
        }
    }
}