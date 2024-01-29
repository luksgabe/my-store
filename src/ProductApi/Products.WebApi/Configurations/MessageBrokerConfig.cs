using Products.Application.Categories.Events;
using Products.Application.Configuration;
using Products.Application.Products.Events;

namespace Products.WebApi.Configurations
{
    public static class MessageBrokerConfig
    {
        public static void AddMessageBrokerConfig(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var rabbitEventBus = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                AddScopedServiceCategory(rabbitEventBus);
                AddScopedServiceProduct(rabbitEventBus);
            }
        }

        private static void AddScopedServiceCategory(IMediatorHandler rabbitEventBus)
        {
            rabbitEventBus.Subscribe<CategoryRegisterEvent, CategoryEventHandler>();
            rabbitEventBus.Subscribe<CategoryUpdateEvent, CategoryEventHandler>();
            rabbitEventBus.Subscribe<CategoryDeleteEvent, CategoryEventHandler>();
        }

        private static void AddScopedServiceProduct(IMediatorHandler rabbitEventBus)
        {
            rabbitEventBus.Subscribe<ProductRegisterEvent, ProductEventHandler>();
            rabbitEventBus.Subscribe<ProductUpdateEvent, ProductEventHandler>();
            rabbitEventBus.Subscribe<ProductDeleteEvent, ProductEventHandler>();
        }
    }
}
