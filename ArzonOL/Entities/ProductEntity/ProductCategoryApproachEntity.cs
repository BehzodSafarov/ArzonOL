namespace ArzonOL.Entities;
public class ProductCategoryApproachEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid? ProductCategoryId { get; set; }
    public virtual ProductCategoryEntity? ProductCategory { get; set; }
    public virtual ICollection<BaseProductEntity>? Products { get; set; }
}
