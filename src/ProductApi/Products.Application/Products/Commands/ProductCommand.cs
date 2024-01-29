using Products.Application.Configuration.Commands;

namespace Products.Application.Products.Commands
{
    public class ProductCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Color { get; protected set; }
        public string Size { get; protected set; }
        public Guid IdCategory { get; protected set; }
        public char? Genre { get; protected set; }
    }
}
