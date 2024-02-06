using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;

namespace Products.Application.Users.Events
{
    public class UserEventHandler : IEventHandler<UserRegisterEvent>
    {
        private readonly IUserRepository _userRepository;

        public UserEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UserRegisterEvent notification, CancellationToken cancellationToken)
        {
            var user = new User(notification.Id, notification.Name, notification.Email, notification.Password);
            await _userRepository.CreateNoSql(user);
        }
    }
}
