using MongoDB.Driver;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Infra.Data.Context;
using Products.Infra.Data.Options;

namespace Products.Infra.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;
        public UserRepository(AppDbContext context, IMongoDatabaseSettings mongoSettings) : base(context, mongoSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);

            _userCollection = database.GetCollection<User>(nameof(User));
            base.MongoDB(_userCollection);
        }
    }
}
