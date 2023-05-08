namespace ArzonOL.Dtos.AuthDtos;

public class ChangePasswordDto
{
    public Guid Id {get; set;}
    public string? NewPassword {get; set;}
    public string? OldPassword {get; set;}
}