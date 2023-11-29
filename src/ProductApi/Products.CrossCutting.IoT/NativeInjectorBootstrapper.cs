using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Categories.Commands;
using Products.Application.Categories.Events;
using Products.Application.Configuration;
using Products.Application.Configuration.Events;
using Products.CrossCutting.Bus;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;
using Products.Infra.Data;
using Products.Infra.Data.Context;
using Products.Infra.Data.EventSourcing;
using Products.Infra.Data.Repositories;
using Products.Infra.Data.Repositories.EventSourcing;

namespace Products.CrossCutting.IoT
{
    public static class NativeInjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            //Event Handlers
            services.AddScoped<INotificationHandler<CategoryRegisterEvent>, CategoryEventHandler>();

            //Command Handlers
            services.AddScoped<IRequestHandler<RegisterCategoryCommand, ValidationResult>, CategoryCommandHandler>();

            //Infra - UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Infra - Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            //Infra - Contexts
            services.AddScoped<AppDbContext>();

            //Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();
        }
    }
}
