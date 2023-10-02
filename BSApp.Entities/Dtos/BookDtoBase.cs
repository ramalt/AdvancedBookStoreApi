using System.ComponentModel.DataAnnotations;

namespace BSApp.Entities.Dtos;
public abstract record BookDtoBase
{
    [Required(ErrorMessage = "Title is a required field.")]
    [MinLength(2, ErrorMessage = "Title must consist of at least 2 characters")]
    [MaxLength(50, ErrorMessage = "Title must consist of at maximum 50 characters")]
    public string Title { get; init; } = null!;

    [Required(ErrorMessage = "Title is a required field.")]
    [Range(0, 1000)]
    public decimal Price { get; init; }

    public BookDtoBase(string title, decimal price)
    {
        Title = title;
        Price = price;
    }
}
