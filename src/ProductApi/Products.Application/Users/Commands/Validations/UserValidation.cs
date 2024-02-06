using FluentValidation;

namespace Products.Application.Users.Commands.Validations
{
    public class UserValidation<T> : AbstractValidator<T> where T : UserCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Please ensure you have entered the Name")
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(1, 255).WithMessage("The Name must have between 1 and 255 characters");
        }

        protected void ValidateEmailSignUp()
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Please ensure you have entered the Email")
                .NotEmpty().WithMessage("Please ensure you have entered the Email")
                .Length(8, 100).WithMessage("The Email must have between 8 and 100 characters")
                .Must(p => p.Contains('.')).WithMessage("The Email is invalid")
                .EmailAddress();
        }

        protected void ValidatePasswordSignUp()
        {
            RuleFor(c => c.Password)
                .NotNull().WithMessage("Please ensure you have entered the Password")
                .NotEmpty().WithMessage("Please ensure you have entered the Password")
                .MinimumLength(10).WithMessage("The Password must have at least 10 characters");
        }

        protected void ValidateEmailSignIn()
        {
            RuleFor(c => c.Email)
                .NotNull().WithMessage("Please ensure you have entered the Email")
                .NotEmpty().WithMessage("Please ensure you have entered the Email");
        }

        protected void ValidatePasswordSignIn()
        {
            RuleFor(c => c.Password)
                .NotNull().WithMessage("Please ensure you have entered the Password")
                .NotEmpty().WithMessage("Please ensure you have entered the Password");
        }
    }
}
