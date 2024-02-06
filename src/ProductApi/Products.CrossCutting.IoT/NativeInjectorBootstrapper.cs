using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Categories.Commands;
using Products.Application.Categories.Events;
using Products.Application.Categories.Queries;
using Products.Application.Categories.Responses;
using Products.Application.Configuration;
using Products.Application.Configuration.Events;
using Products.Application.Products.Commands;
using Products.Application.Products.Events;
using Products.Application.Products.Queries;
using Products.Application.Products.Responses;
using Products.Application.Users.Commands;
using Products.Application.Users.Events;
using Products.Application.Users.Queries;
using Products.Application.Users.Responses;
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
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, RabbitMQueueBus>();

            //Event Handlers
            services.AddTransient<CategoryEventHandler>();
            services.AddTransient<ProductEventHandler>();
            services.AddTransient<UserEventHandler>();

            registerCommandsHandlers(services);

            registerQueryHandlers(services);

            //Infra - UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Infra - Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Infra - Contexts
            services.AddScoped<AppDbContext>();

            //Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();
        }

        private static void registerCommandsHandlers(IServiceCollection services)
        {
            //Category commands
            services.AddScoped<IRequestHandler<RegisterCategoryCommand, ValidationResult>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCategoryCommand, ValidationResult>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCategoryCommand, ValidationResult>, CategoryCommandHandler>();

            //Product commands
            services.AddScoped<IRequestHandler<RegisterProductCommand, ValidationResult>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, ValidationResult>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteProductCommand, ValidationResult>, ProductCommandHandler>();

            //User commands
            services.AddScoped<IRequestHandler<RegisterUserCommand, ValidationResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<LoginUserCommand, ValidationResult>, UserCommandHandler>();
        }

        private static void registerQueryHandlers(IServiceCollection services)
        {
            //Category queries
            services.AddScoped<IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>, CategoryQueryHandler>();
            services.AddScoped<IRequestHandler<GetCategoryByIdQuery, CategoryResponse>, CategoryQueryHandler>();

            //Products queries
            services.AddScoped<IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>, ProductQueryHandler>();
            services.AddScoped<IRequestHandler<GetProductByIdQuery, ProductResponse>, ProductQueryHandler>();

            //User queries
            services.AddScoped<IRequestHandler<GetUserByEmailQuery, UserResponse>, UserQueryHandler>();
        }
    }
}
