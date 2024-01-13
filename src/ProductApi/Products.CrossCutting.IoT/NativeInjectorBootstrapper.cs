using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core.Servers;
using Products.Application.Categories.Commands;
using Products.Application.Categories.Events;
using Products.Application.Categories.Queries;
using Products.Application.Categories.Responses;
using Products.Application.Configuration;
using Products.Application.Configuration.Events;
using Products.CrossCutting.Bus;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;
using Products.Infra.Data;
using Products.Infra.Data.Context;
using Products.Infra.Data.EventSourcing;
using Products.Infra.Data.Options;
using Products.Infra.Data.Repositories;
using Products.Infra.Data.Repositories.EventSourcing;

namespace Products.CrossCutting.IoT
{
    public static class NativeInjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, RabbitMQueueBus>();

            //Event Handlers
            services.AddTransient<CategoryEventHandler>();

            registerCommandsHandlers(services);

            registerQueryHandlers(services);

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

        private static void registerCommandsHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<RegisterCategoryCommand, ValidationResult>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCategoryCommand, ValidationResult>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCategoryCommand, ValidationResult>, CategoryCommandHandler>();
        }

        private static void registerQueryHandlers(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>, CategoryQueryHandler>();
            services.AddScoped<IRequestHandler<GetCategoryByIdQuery, CategoryResponse>, CategoryQueryHandler>();
        }
    }
}
