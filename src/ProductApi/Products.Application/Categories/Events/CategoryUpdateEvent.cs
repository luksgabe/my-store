using Products.Application.Configuration.Messaging;

namespace Products.Application.Categories.Events
{
    public class CategoryUpdateEvent : Event
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public CategoryUpdateEvent(Guid id, string name)
        {
            Id = id;
            AggregateId = id;
            Name = name;
        }
    }
}
