using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.ShoppingCart.API.Data;
using Ecommerce.WebAPI.Core.Usuario;

namespace Ecommerce.ShoppingCart.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<CarrinhoContext>();
        }
    }
}