using MyHomework.API.Entities;
using MyHomework.API.Persistance.Interfaces;

namespace MyHomework.API.Persistance
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
            :base(context)
        {
            _context = context;
        }
    }
}
