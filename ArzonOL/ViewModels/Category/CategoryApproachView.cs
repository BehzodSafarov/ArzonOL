using ArzonOL.ViewModels.Product;

namespace ArzonOL.ViewModels.Category;

public class CategoryApproachView
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid? ProductCategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<ProductView>? Products { get; set; }
    
}