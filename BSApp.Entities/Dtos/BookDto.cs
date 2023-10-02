namespace BSApp.Entities.Dtos;
public record BookDto : BookDtoBase
{
    public int Id { get; init; }
    public BookDto(string title, decimal price) : base(title, price)
    {
    }
}