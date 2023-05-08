using Microsoft.AspNetCore.Identity;

namespace ArzonOL.Entities;

public class UserEntity : IdentityUser
{
    public virtual ICollection<BaseProductEntity>? Products { get; set; }
    public virtual ICollection<ProductVoterEntity>? Voters { get; set; }
    public virtual ICollection<CartEntity>? Carts { get; set; }
    public virtual ICollection<WishListEntity>? WishLists { get; set; } // kak notification
}