using BSApp.Entities.Models;
using BSApp.Entities.RequestFeatures;
using BSApp.Repository.Contracts;

namespace BSApp.Repository.Data;

public interface IBookRepository : IRepositoryBase<Book>
{
    Task<IEnumerable<Book>> GetAllBooksAsync(BookParameters param, bool trackChanges);
    Task<Book> GetOneBookByIdAsync(int id, bool trackChanges);
    void CreateOneBook(Book book);
    void UpdateOneBook(Book book);
    void DeleteOneBook(Book book);
}
