using Products.Application.Configuration.Messaging;

namespace Products.Application.Configuration.Events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}
