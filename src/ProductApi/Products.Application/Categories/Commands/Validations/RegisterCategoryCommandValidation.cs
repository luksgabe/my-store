namespace Products.Application.Categories.Commands.Validations
{
    public class RegisterCategoryCommandValidation : CategoryValidation<RegisterCategoryCommand>
    {
        public RegisterCategoryCommandValidation()
        {
            ValidateName();
        }
    }
}
