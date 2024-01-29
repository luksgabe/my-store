using Products.Application.Products.Commands.Validations;


namespace Products.Application.Products.Commands
{
    public class UpdateProductCommand : ProductCommand
    {
        public UpdateProductCommand(Guid id, string name, string description, string color, string size, char? genre, Guid idCategory)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Size = size;
            IdCategory = idCategory;
            Genre = genre;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
