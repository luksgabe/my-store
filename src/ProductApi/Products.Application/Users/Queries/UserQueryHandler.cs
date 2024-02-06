using AutoMapper;
using MediatR;
using Products.Application.Users.Responses;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;

namespace Products.Application.Users.Queries
{
    public class UserQueryHandler : IRequestHandler<GetUserByEmailQuery, UserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
        {
            User user = (await _userRepository.GetCustomData(p => p.Email == query.Email)).FirstOrDefault();
            return _mapper.Map<UserResponse>(user);
        }
    }
}
