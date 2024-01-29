using Products.Application.Categories.Events;
using Products.Application.Configuration;
using Products.Application.Products.Events;
using System.Reflection;

namespace Products.WebApi.Configurations
{
    public static class MessageBrokerConfig
    {
        public static void AddMessageBrokerConfig(this WebApplication app)
        {
            var currentAssembly = typeof(IMediatorHandler).Assembly;
            var eventHandlersTypes = currentAssembly.GetTypes().Where(t =>
                t.FullName.StartsWith("Products.Application") &&
                t.FullName.EndsWith("EventHandler"));

            foreach (var eventHandlerType in eventHandlersTypes)
            {
                string domain = eventHandlerType.FullName.Split('.')[2];
                if (domain != null)
                {
                    // Find event types in the same namespace as the event handler
                    var eventTypes = currentAssembly.GetTypes().Where(t =>
                        t.FullName.StartsWith($"Products.Application.{domain}") &&
                        t.FullName.EndsWith("Event"));

                    using (var scope = app.Services.CreateScope())
                    {
                        var serviceProvider = scope.ServiceProvider;

                        // Get the IMediatorHandler service
                        var mediatorHandler = serviceProvider.GetRequiredService<IMediatorHandler>();

                        // Subscribe each event type to the corresponding event handler type
                        foreach (var eventType in eventTypes)
                        {
                            // Use reflection to call Subscribe method with generic arguments
                            var method = typeof(IMediatorHandler).GetMethod("Subscribe");
                            var genericMethod = method.MakeGenericMethod(eventType, eventHandlerType);
                            genericMethod.Invoke(mediatorHandler, null);
                        }
                    }
                }

            }


        }
    }
}
