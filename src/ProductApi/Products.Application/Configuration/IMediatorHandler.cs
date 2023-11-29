using FluentValidation.Results;
using Products.Application.Configuration.Commands;
using Products.Application.Configuration.Messaging;
using Products.Domain.Interfaces.SeedWork;

namespace Products.Application.Configuration
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;

        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
