using Products.Application.Categories.Commands.Validations;

namespace Products.Application.Categories.Commands
{
    public class DeleteCategoryCommand : CategoryCommand
    {
        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteCategoryCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
