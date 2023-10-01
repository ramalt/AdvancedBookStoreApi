using BSApp.Entities.Models;
using BSApp.Repository.Data;
using BSApp.Service.Contracts;

namespace BSApp.Service;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILoggerService _logger;

    public BookManager(IRepositoryManager manager, ILoggerService logger)
    {
        _manager = manager;
        _logger = logger;
    }

    public Book CreateBook(Book book)
    {

        _manager.Book.CreateOneBook(book);
        _manager.Save();

        return book;
    }

    public void DeleteBook(int id, bool trackChanges)
    {
        var book = _manager.Book.GetOneBookById(id, trackChanges);
        if (book is null)
        {
            var msg = $"Book not found with id : {id}";
            _logger.LogInfo(msg);
            throw new Exception(msg);
        }


        _manager.Book.Delete(book);
        _manager.Save();

    }

    public IEnumerable<Book> GetAllBooks(bool trackChanges)
    {
        return _manager.Book.GetAllBooks(trackChanges);
    }

    public Book GetBookById(int id, bool trackChanges)
    {
        return _manager.Book.GetOneBookById(id, trackChanges);
    }

    public void UpdateBook(int id, Book book, bool trackChanges)
    {
        var existingBook = _manager.Book.GetOneBookById(id, trackChanges);

        if (existingBook is null)
        {
            var msg = $"Book not found with id : {id}";
            _logger.LogInfo(msg);
            throw new Exception(msg);
        }

        existingBook.Title = book.Title;
        existingBook.Price = book.Price;

        _manager.Book.Update(existingBook);
        _manager.Save();
    }
}
