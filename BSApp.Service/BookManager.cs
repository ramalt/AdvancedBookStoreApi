using BSApp.Entities.Models;
using BSApp.Repository.Data;
using BSApp.Service.Contracts;

namespace BSApp.Service;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;

    public BookManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public Book CreateBook(Book book)
    {
        if (book is null)
        {
            throw new ArgumentNullException(nameof(book));
        }
        _manager.Book.CreateOneBook(book);
        _manager.Save();

        return book;
    }

    public void DeleteBook(int id, bool trackChanges)
    {
        var book = _manager.Book.GetOneBookById(id, trackChanges);
        if (book is null)
            throw new Exception($"Book not found with id : {id}");

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
            throw new Exception($"Book not found with id: {id}");

        if (book is null)
            throw new ArgumentNullException(nameof(book));

        existingBook.Title = book.Title;
        existingBook.Price = book.Price;

        _manager.Book.Update(existingBook);
        _manager.Save();
    }
}
