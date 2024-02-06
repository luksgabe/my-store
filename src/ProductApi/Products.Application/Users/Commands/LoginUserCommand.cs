using Products.Application.Users.Commands.Validations;

namespace Products.Application.Users.Commands
{
    public class LoginUserCommand : UserCommand
    {
        public LoginUserCommand(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new LoginUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
