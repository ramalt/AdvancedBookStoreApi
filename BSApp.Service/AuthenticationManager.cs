using AutoMapper;
using BSApp.Entities.Dtos.User;
using BSApp.Entities.Models;
using BSApp.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BSApp.Service;

public class AuthenticationManager : IAuthenticationService
{
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthenticationManager(ILoggerService logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterUser(RegisterUserDto registerUserDto)
    {
        User user = _mapper.Map<User>(registerUserDto);

        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if(result.Succeeded)
            await _userManager.AddToRolesAsync(user, registerUserDto.Roles);
        
        return result;
        
    }
}
