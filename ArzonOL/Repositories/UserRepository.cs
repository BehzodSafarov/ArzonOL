using ArzonOL.Data;
using ArzonOL.Entities;

namespace ArzonOL.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}