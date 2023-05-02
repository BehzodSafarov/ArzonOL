namespace ArzonOL.Entities;
public class WishListEntity : BaseEntity
{
    public Guid? UserId { get; set; }
    public UserEntity? User { get; set; }
    public ICollection<BaseProductEntity>? Products { get; set; }
}
