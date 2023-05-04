using ArzonOL.Data;
using ArzonOL.Entities;
using ArzonOL.Repositories.Interfaces;

namespace ArzonOL.Repositories;

public class VoterRepository : GenericRepository<ProductVoterEntity>, IVoterRepository
{
    public VoterRepository(AppDbContext context) : base(context)
    {
    }
}