using Microsoft.AspNetCore.Identity;

namespace ArzonOL.Entities;

public class UserEntity : IdentityUser
{
    public ICollection<ProductVoter>? Voters { get; set; }
    public ICollection<CartEntity>? Carts { get; set; }
    public ICollection<WishListEntity>? WishLists { get; set; }
}