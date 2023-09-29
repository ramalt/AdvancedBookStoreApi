namespace BSApp.Repository.Data;

public interface IRepositoryManager
{
    IBookRepository Book { get; }

    void Save();
}
