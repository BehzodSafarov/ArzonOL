namespace ArzonOL.Entities;

public class ProductCategoryEntity : BaseEntity
{
   public string? Name { get; set; }
   public string? Description { get; set; }
   public virtual ICollection<ProductCategoryApproachEntity>? Approaches { get; set; }
}
