using MediatR;

namespace Products.Application.Categories.Events
{
    public class CategoryEventHandler : INotificationHandler<CategoryRegisterEvent>
    {
        public Task Handle(CategoryRegisterEvent notification, CancellationToken cancellationToken)
        {
            // do something...
            Console.WriteLine("Register category event worked!");
            return Task.CompletedTask;
        }
    }
}
