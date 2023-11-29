using Products.Application.Categories.Commands.Validations;

namespace Products.Application.Categories.Commands
{
    public class RegisterCategoryCommand : CategoryCommand
    {
        public RegisterCategoryCommand(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterCategoryCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
