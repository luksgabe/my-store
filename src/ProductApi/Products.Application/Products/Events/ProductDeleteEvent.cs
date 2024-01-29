using Products.Application.Configuration.Messaging;

namespace Products.Application.Products.Events
{
    public class ProductDeleteEvent : Event
    {
        public ProductDeleteEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
