using Microsoft.AspNetCore.Identity;

namespace ArzonOL.Entities;

public class UserEntity : IdentityUser
{
    public ICollection<BaseProductEntity>? Products { get; set; }
    public ICollection<ProductVoterEntity>? Voters { get; set; }
    public ICollection<CartEntity>? Carts { get; set; }
    public ICollection<WishListEntity>? WishLists { get; set; }
    public string? Role { get; set; }
}