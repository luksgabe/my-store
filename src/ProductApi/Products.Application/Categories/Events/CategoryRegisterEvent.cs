using Products.Application.Configuration.Messaging;
using Productss.Domain.Entities;

namespace Products.Application.Categories.Events
{
    public class CategoryRegisterEvent : Event
    {
        public CategoryRegisterEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

    }
}
