using Newtonsoft.Json;
using Products.Application.Configuration.Events;
using Products.Application.Configuration.Messaging;
using Products.Infra.Data.Repositories.EventSourcing;

namespace Products.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public SqlEventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Save<T>(T theEvent) where T : Event
        {


            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData);

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
