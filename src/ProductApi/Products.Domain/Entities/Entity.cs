using Products.Domain.Interfaces.SeedWork;

namespace Products.Domain.Entities
{
    public abstract class Entity
    {
        private List<IEvent> _domainEvents;

        public virtual Guid Id { get; protected set; }
        public abstract DateTime? UpdatedAt { get; protected set; }
        public abstract DateTime? DeletedAt { get; protected  set; }

        public IReadOnlyCollection<IEvent> DomainEvents => _domainEvents?.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void Update()
        {
            this.UpdatedAt = DateTime.Now;
        }

        public void Delete()
        {
            this.DeletedAt = DateTime.Now;
        }

        public void AddDomainEvent(IEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(IEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public override bool Equals(object obj)
        {
            Entity entity = obj as Entity;
            if ((object)this == entity)
            {
                return true;
            }

            if ((object)entity == null)
            {
                return false;
            }

            return Id.Equals(entity.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if ((object)a == null && (object)b == null)
            {
                return true;
            }

            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() ^ 0x5D) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
