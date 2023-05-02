namespace ArzonOL.Entities;

public class CartEntity : BaseEntity
{
    public Guid? UserId { get; set; }
    public UserEntity? User { get; set; }
    public ICollection<BaseProductEntity>? Products { get; set; }
}