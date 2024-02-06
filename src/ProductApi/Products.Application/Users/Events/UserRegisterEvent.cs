using Products.Application.Configuration.Messaging;

namespace Products.Application.Users.Events
{
    public class UserRegisterEvent : Event
    {
        public UserRegisterEvent(Guid id, string name, string email, string password)
        {
            Id=id;
            AggregateId = id;
            Name=name;
            Email=email;
            Password=password;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}
