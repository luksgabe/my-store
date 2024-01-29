using Products.Application.Configuration.Messaging;

namespace Products.Application.Products.Events
{
    public class ProductUpdateEvent : Event
    {
        public ProductUpdateEvent(Guid id, string name, string? description, string? color, string? size, Guid idCategory, char? genre)
        {
            Id=id;
            Name=name;
            Description=description;
            Color=color;
            Size=size;
            IdCategory=idCategory;
            Genre=genre;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? Color { get; private set; }
        public string? Size { get; private set; }
        public Guid IdCategory { get; private set; }
        public char? Genre { get; private set; }
    }
}
