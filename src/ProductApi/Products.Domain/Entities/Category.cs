using Products.Domain.Entities;
using Products.Domain.Interfaces.SeedWork;

namespace Productss.Domain.Entities
{
    public class Category : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public override DateTime? UpdatedAt { get; set; }
        public override DateTime? DeletedAt { get; set; }

        public ICollection<Product> Products { get; set; }

        public Category(string name)
        {
            Name = name;
        }
    }
}
