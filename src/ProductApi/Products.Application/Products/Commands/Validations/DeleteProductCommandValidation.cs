namespace Products.Application.Products.Commands.Validations
{
    public class DeleteProductCommandValidation : ProductValidation<DeleteProductCommand>
    {
        public DeleteProductCommandValidation() 
        {
            ValidateId();
        }
    }
}
