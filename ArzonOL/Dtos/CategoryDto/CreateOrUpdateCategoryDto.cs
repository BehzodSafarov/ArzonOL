using System.ComponentModel.DataAnnotations;

namespace ArzonOL.Dtos.CategoryDto;

public class CreateOrUpdateCategoryDto
{
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
}