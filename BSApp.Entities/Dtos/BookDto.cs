namespace BSApp.Entities.Dtos;
public record BookDto
{
    public string Title { get; init; }
    public decimal Price { get; init; }
}