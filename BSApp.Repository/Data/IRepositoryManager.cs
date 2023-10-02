namespace BSApp.Repository.Data;

public interface IRepositoryManager
{
    IBookRepository Book { get; }

    Task SaveAsync();
}
