using System.ComponentModel.DataAnnotations;

namespace ArzonOL.Dtos.CategoryDto;

public class CreateOrUpdateCategoryApproachDto
{
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid? ProductCategoryId { get; set; }
}