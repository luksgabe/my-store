namespace Products.Application.Users.Commands.Validations
{
    public class LoginUserCommandValidation : UserValidation<LoginUserCommand>
    {
        public LoginUserCommandValidation()
        {
            ValidateEmailSignIn();
            ValidatePasswordSignIn();
        }
    }
}
