using Products.Domain.Interfaces.SeedWork;

namespace Products.Domain.Entities
{
    public class Genre : Entity, IAggregateRoot
    {
        public string Description { get; private set; }
        public override DateTime? UpdatedAt { get; set; }
        public override DateTime? DeletedAt { get; set; }

        public ICollection<Product> Products { get; set; }

        public Genre(string description)
        { 
            Description = description;
        }
    }
}
