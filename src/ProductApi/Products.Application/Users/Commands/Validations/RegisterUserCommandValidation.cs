namespace Products.Application.Users.Commands.Validations
{
    public class RegisterUserCommandValidation : UserValidation<RegisterUserCommand>
    {
        public RegisterUserCommandValidation()
        {
            ValidateName();
            ValidateEmailSignUp();
            ValidatePasswordSignUp();
        }
    }
}
