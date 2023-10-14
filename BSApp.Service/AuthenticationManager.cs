using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BSApp.Entities.Dtos;
using BSApp.Entities.Dtos.User;
using BSApp.Entities.Exceptions;
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

        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, registerUserDto.Roles);

        return result;

    }

    public async Task<bool> ValidateUser(AuthenticateUserDto authUserDto)
    {
        _user = await _userManager.FindByNameAsync(authUserDto.Username);
        bool isPasswordCorrect = await _userManager.CheckPasswordAsync(_user, authUserDto.Password);

        bool result = _user is not null && isPasswordCorrect;

        if (!result)
            _logger.LogWarning("Authentication failed. Wrong credentials");

        return result;

    }

    public async Task<TokenDto> CreateToken(bool populateExpire)
    {
        var signinCredentials = GetSigningCredentials();
        var userClaims = await GetUserClaimsAsync();
        var tokenOptions = GenerateTokenOptions(signinCredentials, userClaims);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        var refreshToken = GenerateRefreshToken();

        _user.RefreshToken = refreshToken;

        if (populateExpire)
        {
            _user.RefreshTokenExpireTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(_user);
        }

        TokenDto tokenDto = new()
        {
            AccesToken = accessToken,
            RefreshToken = refreshToken
        };

        return tokenDto;
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
            issuer: jwtSettings["issuer"],
            audience: jwtSettings["audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: credentials
        );
    }

    private string GenerateRefreshToken()
    {
        var random = new byte[32];

        using var range = RandomNumberGenerator.Create();

        range.GetBytes(random);

        return Convert.ToBase64String(random);
    }

    public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
    {
        var principal = GetClaimsPrincipalFromExpiredToken(tokenDto.AccesToken);
        var existingUser = await _userManager.FindByNameAsync(principal.Identity.Name);

        if (existingUser is null
           || existingUser.RefreshToken != tokenDto.RefreshToken
           || existingUser.RefreshTokenExpireTime <= DateTime.Now)
            throw new RefreshTokenBadRequest();

        _user = existingUser;

        return await CreateToken(false);



    }

    private ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var tokenValidationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["issuer"],
            ValidAudience = jwtSettings["audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secret"]))
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParams, out SecurityToken securityToken);


        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("invalid token.");

        return principal;

    }


}
