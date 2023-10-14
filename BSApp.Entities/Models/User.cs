using Microsoft.AspNetCore.Identity;

namespace BSApp.Entities.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }
}
