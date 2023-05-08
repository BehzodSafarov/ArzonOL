using System.ComponentModel.DataAnnotations;

namespace ArzonOL.Dtos.AuthDtos;

public class RegisterDto
{
    [Required]
    public string? UserName { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? Email { get; set; }
}