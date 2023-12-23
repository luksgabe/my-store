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


        public Guid IdCategory { get; set; }
        public Category Category { get; set; }
        public Guid? IdGenre { get; set; }
        public Genre Genre { get; set; }

        public Product(Guid id, string name, string description, string color, string size)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Size = size;
        }
    }
}
