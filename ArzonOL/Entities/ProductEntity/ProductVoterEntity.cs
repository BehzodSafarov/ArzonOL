namespace ArzonOL.Entities;

public class ProductVoterEntity : BaseEntity
{
    public int Vote { get; set; }
    public string? Comment { get; set; }
    public Guid ProductId { get; set; }
    public virtual BaseProductEntity? Product { get; set; }
    public Guid? UserId { get; set; }
    public virtual UserEntity? User { get; set; }
}
