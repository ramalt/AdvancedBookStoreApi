using BSApp.Entities.Dtos;
using BSApp.Entities.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace BSApp.Service.Contracts;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(RegisterUserDto registerUserDto);

    Task<bool> ValidateUser(AuthenticateUserDto authUserDto);
    Task<string> CreateToken();
}
