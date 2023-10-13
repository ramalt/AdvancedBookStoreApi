using System.ComponentModel.DataAnnotations;

namespace BSApp.Entities.Dtos.User;

public record RegisterUserDto
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }

    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; init; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; init; } = null!;
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public ICollection<String>? Roles { get; init; }
    

}
