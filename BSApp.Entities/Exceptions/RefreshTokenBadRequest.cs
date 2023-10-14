namespace BSApp.Entities.Exceptions;

public class RefreshTokenBadRequest : BadRequestException
{
    public RefreshTokenBadRequest() : base($"Invalid token request. tokenDto has invalid values")
    {
        
    }    
}
