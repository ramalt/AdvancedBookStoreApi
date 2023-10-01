namespace BSApp.Entities.Exceptions;

public sealed class BookNotFoundException : NotFoundException
{
    public BookNotFoundException(int id) : base($"Book  could not found with id: {id}.")
    {
    }
}
