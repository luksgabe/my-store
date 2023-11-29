using Products.Domain.Interfaces.SeedWork;
using Productss.Domain.Entities;

namespace Products.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? Color { get; private set; }
        public string? Size { get; private set; }
        public override DateTime? UpdatedAt { get; set; }
        public override DateTime? DeletedAt { get; set; }


        public long IdCategory { get; set; }
        public Category Category { get; set; }
        public long? IdGenre { get; set; }
        public Genre Genre { get; set; }

        public Product(string name, string description, string color, string size)
        {
            Name = name;
            Description = description;
            Color = color;
            Size = size;
        }
    }
}
