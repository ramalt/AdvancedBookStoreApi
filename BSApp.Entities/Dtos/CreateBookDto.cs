namespace BSApp.Entities.Dtos;

public record CreateBookDto : BookDtoBase
{
    public CreateBookDto(string title, decimal price) : base(title, price)
    {
    }
}
