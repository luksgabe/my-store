using Products.Application.Configuration.Messaging;

namespace Products.Application.Categories.Events
{
    public class CategoryRegisterEvent : Event
    {
        public CategoryRegisterEvent(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; set; }

        public string Name { get; private set; }

        
    }
}
