using Products.Application.Configuration.Commands;

namespace Products.Application.Categories.Commands
{
    public class CategoryCommand : Command
    {
        public long Id { get; protected set; }
        public string Name { get; protected set; }
    }
}
