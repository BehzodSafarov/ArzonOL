namespace ArzonOL.Entities;


public class ProductMediaEntity : BaseEntity
{
    public string? ImageName { get; set; }
    public string? ImagePath { get; set; }
    public Guid? ProductId { get; set; }
    public BaseProductEntity? Product { get; set; }
}