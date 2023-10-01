namespace BSApp.Api.Exceptions;

public sealed class BookNotFound : NotFound
{
    public BookNotFound(int id) : base($"Book  could not found with id: {id}.")
    {
    }
}
