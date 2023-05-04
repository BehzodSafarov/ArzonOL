using ArzonOL.Data;
using ArzonOL.Entities;
using ArzonOL.Repositories.Interfaces;

namespace ArzonOL.Repositories;

public class CategoryApproachRepository : GenericRepository<ProductCategoryApproachEntity>, ICategoryApproachRepository
{
    public CategoryApproachRepository(AppDbContext context) : base(context)
    {
    }
}
