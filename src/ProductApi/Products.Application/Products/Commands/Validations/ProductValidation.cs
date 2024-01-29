using FluentValidation;

namespace Products.Application.Products.Commands.Validations
{
    public class ProductValidation<T> : AbstractValidator<T> where T : ProductCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(1, 255).WithMessage("The Name must have between 1 and 255 characters");
        }

        protected void ValidationColor()
        {
            RuleFor(c => c.Color)
                .MaximumLength(10).WithMessage("The color cannot have more 10 characters");
        }

        protected void ValidationSize()
        {
            RuleFor(c => c.Size)
                .MaximumLength(5).WithMessage("The size cannot have more 5 characters");
        }
        
        protected void ValidateGender()
        {
            RuleFor(c => c.Genre)
                .Must(m =>
                {
                    var isValid = true;
                    if (m.HasValue)
                    {
                        var valueString = m.Value.ToString().ToLower();
                        if (valueString != "m" && valueString != "f")
                            isValid = false;
                    }
                    return isValid;
                })
                .WithMessage("The value of gender must be whether m or b");
        }
    }
}
