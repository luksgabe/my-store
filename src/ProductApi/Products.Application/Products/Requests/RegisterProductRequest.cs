namespace Products.Application.Products.Requests
{
    public class RegisterProductRequest
    {
        public RegisterProductRequest(string name, string? description, string? color, string? size, Guid idCategory, char? genre)
        {
            Name = name;
            Description = description;
            Color = color;
            Size = size;
            IdCategory = idCategory;
            Genre = genre;
        }

        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? Color { get; private set; }
        public string? Size { get; private set; }
        public char? Genre { get; private set; }
        public Guid IdCategory { get; private set; }
    }
}
