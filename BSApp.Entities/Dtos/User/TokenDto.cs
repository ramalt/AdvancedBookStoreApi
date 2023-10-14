namespace BSApp.Entities.Dtos.User;

public record TokenDto
{
    public string AccesToken { get; init; }
    public string RefreshToken { get; init; }
}
