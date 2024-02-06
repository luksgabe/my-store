using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Products.Application.Configuration.Commands;
using Products.Application.Configuration.Messaging;
using Products.Application.Users.Events;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;

namespace Products.Application.Users.Commands
{
    public class UserCommandHandler : CommandHandlerBase,
        IRequestHandler<RegisterUserCommand, ValidationResult>,
        IRequestHandler<LoginUserCommand, ValidationResult>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository) : base(unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> Handle(RegisterUserCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var userExists = await _userRepository.GetCustomData(p => p.Email == message.Email);
            if(userExists.Any())
            {
                AddError("This user already exists.");
                return ValidationResult;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(message.Password);

            var user = new User(Guid.NewGuid(), message.Name, message.Email, passwordHash);

            user.AddDomainEvent(new UserRegisterEvent(user.Id, user.Name, user.Email, user.Password));

            await _userRepository.AddAsync(user);

            return await Commit();
        }

        public async Task<ValidationResult> Handle(LoginUserCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var user = (await _userRepository.GetCustomData(p => p.Email == message.Email)).FirstOrDefault();
            if (user != null)
            {
                if(!BCrypt.Net.BCrypt.Verify(message.Password, user.Password))
                {
                    AddError("Invalid Email or Password");                    
                }
            }
            else
            {
                AddError("Invalid Email or Password");
            }

            return ValidationResult;
        }
    }
}
