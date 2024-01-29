using Products.Application.Products.Commands.Validations;

namespace Products.Application.Products.Commands
{
    public class DeleteProductCommand : ProductCommand
    {
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
