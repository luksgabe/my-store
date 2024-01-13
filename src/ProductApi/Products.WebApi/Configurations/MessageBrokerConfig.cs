using Products.Application.Categories.Events;
using Products.Application.Configuration;


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
            }
        }

        private static void AddScopedServiceCategory(IMediatorHandler rabbitEventBus)
        {
            rabbitEventBus.Subscribe<CategoryRegisterEvent, CategoryEventHandler>();
            rabbitEventBus.Subscribe<CategoryUpdateEvent, CategoryEventHandler>();
            rabbitEventBus.Subscribe<CategoryDeleteEvent, CategoryEventHandler>();
        }
    }
}
