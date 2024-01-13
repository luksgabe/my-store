using Products.Application.Categories.Commands.Validations;

namespace Products.Application.Categories.Commands
{
    public class UpdateCategoryCommand : CategoryCommand
    {
        public UpdateCategoryCommand(Guid id, string name)
        {
            Id = id;
            Name = name;            
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCategoryCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
