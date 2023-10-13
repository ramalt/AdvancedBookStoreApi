using Microsoft.AspNetCore.Identity;

namespace BSApp.Entities.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
