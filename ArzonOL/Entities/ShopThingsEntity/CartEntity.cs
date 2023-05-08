namespace ArzonOL.Entities;

public class CartEntity : BaseEntity
{
    public Guid? UserId { get; set; }
    public virtual UserEntity? User { get; set; }
    public virtual ICollection<BaseProductEntity>? Products { get; set; }
}