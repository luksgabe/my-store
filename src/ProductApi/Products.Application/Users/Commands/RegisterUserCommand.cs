using FluentValidation;
using Products.Application.Users.Commands.Validations;

namespace Products.Application.Users.Commands
{
    public class RegisterUserCommand : UserCommand
    {
        public RegisterUserCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
