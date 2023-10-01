namespace BSApp.Entities.Dtos;
public record BookDto
{
    public string Title { get; init; } = null!;
    public decimal Price { get; init; }
}