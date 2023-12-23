using Products.CrossCutting.Bus;
using Products.CrossCutting.IoT;

namespace Products.WebApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var rabbitMqSettings = configuration.GetSection("RabbitMqSettings").Get<RabbitMqSetting>();
            services.AddSingleton(rabbitMqSettings);

            NativeInjectorBootstrapper.RegisterServices(services, configuration);
        }
    }
}
