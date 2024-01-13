using Products.Domain.Interfaces.Repositories;

namespace Products.Application.Categories.Commands.Validations
{
    public class DeleteCategoryCommandValidation : CategoryValidation<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidation()
        {
            ValidateId();
        }
    }
}
