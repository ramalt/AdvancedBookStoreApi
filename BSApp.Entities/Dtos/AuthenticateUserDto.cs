using System.ComponentModel.DataAnnotations;

namespace BSApp.Entities.Dtos;

public record AuthenticateUserDto
{
    [Required(ErrorMessage ="Username is required.")]
    public string Username { get; init; } = null!;

    [Required(ErrorMessage ="Password is required.")]
    public string Password { get; init; } = null!;  
}
