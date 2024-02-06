using AutoMapper;
using Products.Application.Users.Responses;
using Products.Domain.Entities;

namespace Products.Application.Users.Map
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserResponse>().ReverseMap();
        }
    }
}
