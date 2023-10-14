using BSApp.Entities.Dtos;
using BSApp.Entities.Dtos.User;
using BSApp.Presentation.ActionFilters;
using BSApp.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BSApp.Presentation.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _service;

    public AuthenticationController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
    {
        var result = await _service.AuthenticationService.RegisterUser(registerUserDto);

        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
            {
                ModelState.TryAddModelError(err.Code, err.Description);    
            }
            return BadRequest(ModelState);
        }

        return StatusCode(201);

    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserDto authenticateUserDto)
    {
        var isUserValid = await _service.AuthenticationService.ValidateUser(authenticateUserDto);
        if(isUserValid)
        {
            var token = await  _service.AuthenticationService.CreateToken(true);

            return Ok(token);
        }

        return Unauthorized();
    }

    [HttpPost("refresh")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
    {
        var token = await _service.AuthenticationService.RefreshToken(tokenDto);

        return Ok(token);
    }

}
