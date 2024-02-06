using Products.Domain.Interfaces.SeedWork;

namespace Products.Domain.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public override DateTime? UpdatedAt { get; protected set; }
        public override DateTime? DeletedAt { get; protected set; }

        public User(Guid id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
