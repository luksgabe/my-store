using Products.Domain.Interfaces.Repositories;

namespace Products.Application.Products.Commands.Validations
{
    public class UpdateProductCommandValidation : ProductValidation<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidationColor();
            ValidationSize();
            ValidateGender();
        }
    }
}
