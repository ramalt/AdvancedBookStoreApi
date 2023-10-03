namespace BSApp.Entities.Exceptions;

public class PriceOutOfRangeBadRequest : BadRequestException
{
    public PriceOutOfRangeBadRequest() : base("Max price should be less than 1000 and greater than 0.")
    {
    }
}
