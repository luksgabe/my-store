namespace Products.Application.Categories.Commands.Validations
{
    public class UpdateCategoryCommandValidation : CategoryValidation<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}
