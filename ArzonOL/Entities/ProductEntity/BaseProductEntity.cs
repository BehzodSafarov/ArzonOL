using System.ComponentModel.DataAnnotations.Schema;
using ArzonOL.Enums;

namespace ArzonOL.Entities;

public class BaseProductEntity : BaseEntity
{
    public string? Name { get; set; }
    public long OldPrice { get; set; }
    public long NewPrice { get; set; }
    public double? Discount { get; set; }
    public ICollection<ProductMediaEntity>? ProductMedias { get; set; }
    public string? VideoUrl { get; set; }
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public long Latitudes { get; set; }
    public long Longitudes { get; set; }
    public ERegion Region { get; set; }
    public string? Destrict { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<ProductVoter>? Voters { get; set; }
    public long BoughtCount { get; set; }
    [ForeignKey(nameof(Merchant))]
    public string? MerchantId { get; set; }
    public MerchandEntity? Merchant { get; set; }
    public Guid? ProductCategoryApproachId { get; set; }
    public ProductCategoryApproachEntity? ProductCategoryApproach { get; set; }
    public Guid CardId { get; set; }
    public CartEntity? Cart { get; set; }
    public Guid? WishListId { get; set; }
    public WishListEntity? WishList { get; set; }
}