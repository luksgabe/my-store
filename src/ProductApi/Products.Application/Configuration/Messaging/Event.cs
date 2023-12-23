using MediatR;
using Products.Domain.Interfaces.SeedWork;

namespace Products.Application.Configuration.Messaging
{
    public abstract class Event : Message, IEvent, IEventHandler
    {
        public DateTime Timestamp { get; private set; }
        protected Event()
        {
            Timestamp = DateTime.Now;
        }
       
    }
}
