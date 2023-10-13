using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BSApp.Entities.Dtos;
using BSApp.Entities.Dtos.User;
using BSApp.Entities.Models;
using BSApp.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BSApp.Service;

public class AuthenticationManager : IAuthenticationService
{
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private User? _user;

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

    public async Task<bool> ValidateUser(AuthenticateUserDto authUserDto)
    {
        _user = await _userManager.FindByNameAsync(authUserDto.Username);
        bool isPasswordCorrect = await _userManager.CheckPasswordAsync(_user, authUserDto.Password);

        bool result = _user is not null && isPasswordCorrect; 
    
        if(!result)
            _logger.LogWarning("Authentication failed. Wrong credentials");
        
        return result;

    }

    public async Task<string> CreateToken()
    {
        var signinCredentials = GetSigningCredentials();
        var userClaims = await GetUserClaimsAsync();
        var tokenOptions = GenerateTokenOptions(signinCredentials, userClaims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secret"]));
        return new(key, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetUserClaimsAsync()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName)
        };

        var roles = await _userManager.GetRolesAsync(_user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        return new JwtSecurityToken(
            issuer : jwtSettings["issuer"],
            audience: jwtSettings["audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: credentials
        );
    }


}
