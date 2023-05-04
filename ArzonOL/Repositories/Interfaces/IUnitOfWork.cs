namespace ArzonOL.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICategoryApproachRepository CategoryApproachRepository { get; }
    IVoterRepository VoterRepository { get; }
    IProductMediaRepository ProductMediaRepository { get; }
    IProductRepository ProductRepository { get; }
    IUserRepository UserRepository { get; }
    Task<int> SaveAsync();
}