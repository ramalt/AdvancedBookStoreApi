using BSApp.Entities.Models;
using BSApp.Repository.Contracts;
using BSApp.Repository.Data;

namespace BSApp.Repository.Data;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(ApplicationContext context) : base(context)
    {
    }
    public void CreateOneBook(Book book) => Create(book);
    public void DeleteOneBook(Book book) => Delete(book);
    public void UpdateOneBook(Book book) => Update(book);
    public IQueryable<Book> GetAllBooks(bool trackChanges) => FindAll(trackChanges);
    public Book GetOneBookById(int id, bool trackChanges) => FindByCondition(b => b.Id == id, trackChanges).SingleOrDefault();
}
