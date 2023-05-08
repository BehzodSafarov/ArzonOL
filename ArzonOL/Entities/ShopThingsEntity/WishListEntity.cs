namespace ArzonOL.Entities;
public class WishListEntity : BaseEntity
{
    public Guid? UserId { get; set; }
    public virtual UserEntity? User { get; set; }
    public virtual ICollection<BaseProductEntity>? Products { get; set; }
}
