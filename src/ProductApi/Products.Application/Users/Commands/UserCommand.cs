using Products.Application.Configuration.Commands;

namespace Products.Application.Users.Commands
{
    public class UserCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
    }
}
