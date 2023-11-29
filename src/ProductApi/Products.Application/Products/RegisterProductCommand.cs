using Products.Application.Configuration.Commands;
using Products.Application.Products.DTOs;

namespace Products.Application.Products
{
    public class RegisterProductCommand : CommandBase<ProductDTO>
    {
        public string Name { get; }
        public string Description { get; }
        public string Color { get; }
        public string Size { get; }
        public RegisterProductCommand(string name, string description, string color, string size)
        {
            Name = name;
            Description = description;
            Color = color;
            Size = size;
        }
    }
}
