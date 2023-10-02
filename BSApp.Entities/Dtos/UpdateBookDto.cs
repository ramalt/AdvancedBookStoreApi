namespace BSApp.Entities.Dtos;

public record UpdateBookDto : BookDtoBase
{
    public UpdateBookDto(string title, decimal price) : base(title, price)
    {
    }
}
