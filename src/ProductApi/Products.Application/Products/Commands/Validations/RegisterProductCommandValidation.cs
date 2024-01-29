namespace Products.Application.Products.Commands.Validations
{
    public class RegisterProductCommandValidation : ProductValidation<RegisterProductCommand>
    {
        public RegisterProductCommandValidation() 
        {
            ValidateName();
            ValidationColor();
            ValidationSize();
            ValidateGender();
        }
    }
}
