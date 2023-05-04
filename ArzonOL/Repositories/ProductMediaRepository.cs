using ArzonOL.Data;
using ArzonOL.Entities;
using ArzonOL.Repositories.Interfaces;

namespace ArzonOL.Repositories;

public class ProductMediaRepository : GenericRepository<ProductMediaEntity>, IProductMediaRepository
{
    public ProductMediaRepository(AppDbContext context) : base(context)
    {
    }
}