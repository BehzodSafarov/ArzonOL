using ArzonOL.Data;
using ArzonOL.Repositories.Interfaces;

namespace ArzonOL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ICategoryApproachRepository CategoryApproachRepository { get; }
    public IVoterRepository VoterRepository  { get; }
    public IProductMediaRepository ProductMediaRepository  { get; }
    public IProductRepository ProductRepository  { get; }
    public IUserRepository UserRepository  { get; }
    public ICategoryRepository CategoryRepository { get; }

    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        CategoryApproachRepository = new CategoryApproachRepository(_context);
        VoterRepository = new VoterRepository(_context);
        ProductMediaRepository = new ProductMediaRepository(_context);
        ProductRepository = new ProductRepository(_context);
        UserRepository = new UserRepository(_context);
        CategoryRepository = new CategoryRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}