using ArzonOL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArzonOL.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity>? Users { get; set; }
    public DbSet<MerchandEntity>? Merchands { get; set; }
    public DbSet<BaseProductEntity>? Products { get; set; }
    public DbSet<CartEntity>? Carts { get; set; }
    public DbSet<WishListEntity>? WishLists { get; set; }
    public DbSet<ProductVoter>? ProductVoters { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}