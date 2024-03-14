using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Core.Utils;
using Ecommerce.MessageBus;
using Ecommerce.Order.API.Services;

namespace Ecommerce.Order.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<PedidoOrquestradorIntegrationHandler>()
                .AddHostedService<PedidoIntegrationHandler>();
        }
    }
}