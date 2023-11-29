using Microsoft.EntityFrameworkCore;
using Products.Application.Configuration;
using Products.Application.Configuration.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.SeedWork;
using Products.Infra.Data.Context;

namespace Products.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appContext;
        private readonly IMediatorHandler _mediatorHandler;

        public UnitOfWork(AppDbContext appContext, IMediatorHandler mediatorHandler)
        {
            this._appContext = appContext;
            this._mediatorHandler = mediatorHandler;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediatorHandler.PublishDomainEvents(_appContext).ConfigureAwait(false);

            return await this._appContext.SaveChangesAsync(cancellationToken);
        }

    }

    public static class MediatorExtension
    {
        public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent((Event)domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
