using Products.Application.Configuration.Queries;
using Products.Application.Users.Responses;

namespace Products.Application.Users.Queries
{
    public class GetUserByEmailQuery : IQuery<UserResponse>
    {
        public GetUserByEmailQuery(string email)
        {
            this.Email = email;
        }
        public string Email { get; private set; }

    }
}
