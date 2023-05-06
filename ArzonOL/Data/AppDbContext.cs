using ArzonOL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArzonOL.Data;

public class AppDbContext : IdentityDbContext<UserEntity>
{
    public DbSet<BaseProductEntity>? Products { get; set; }
    public DbSet<CartEntity>? Carts { get; set; }
    public DbSet<WishListEntity>? WishLists { get; set; }
    public DbSet<ProductVoterEntity>? ProductVoters { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // comment
    }

}