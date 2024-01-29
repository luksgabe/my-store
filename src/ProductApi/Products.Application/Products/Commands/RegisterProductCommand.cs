using Products.Application.Products.Commands.Validations;


namespace Products.Application.Products.Commands
{
    public class RegisterProductCommand : ProductCommand
    {
        
        public RegisterProductCommand(string name, string description, string color, string size, char? genre, Guid idCategory)
        {
            Name = name;
            Description = description;
            Color = color;
            Size = size;
            IdCategory = idCategory;
            Genre = genre;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
