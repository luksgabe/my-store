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
        public override DateTime? UpdatedAt { get; protected set; }
        public override DateTime? DeletedAt { get; protected set; }
        public char? Genre { get; private set; }
        public Guid IdCategory { get; private set; }
        public Category Category { get; private set; }

        public Product(Guid id, string name, string description, string color, string size, Guid idCategory, char? genre)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Size = size;
            IdCategory = idCategory;
            Genre = genre;
        }

        public void SetCategory(Category category) => this.Category = category;
    }
}
