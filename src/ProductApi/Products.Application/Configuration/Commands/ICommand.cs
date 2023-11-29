using MediatR;

namespace Products.Application.Configuration.Commands
{
    public interface ICommand : IRequest
    {
        long Id { get; }
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
        long Id { get; }
    }
}
