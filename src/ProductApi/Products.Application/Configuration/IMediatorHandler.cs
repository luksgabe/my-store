using FluentValidation.Results;
using MediatR;
using Products.Application.Configuration.Commands;
using Products.Application.Configuration.Messaging;
using Products.Application.Configuration.Queries;
using Products.Domain.Interfaces.SeedWork;

namespace Products.Application.Configuration
{
    public interface IMediatorHandler
    {
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;

        Task<TResponse> SendQuery<TResponse>(IRequest<TResponse> request);
        Task PublishEvent<T>(T @event) where T : Event;

        void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;
    }
}
