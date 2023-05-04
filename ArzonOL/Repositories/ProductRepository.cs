using ArzonOL.Data;
using ArzonOL.Entities;
using ArzonOL.Repositories.Interfaces;

namespace ArzonOL.Repositories;
public class ProductRepository : GenericRepository<BaseProductEntity>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}
