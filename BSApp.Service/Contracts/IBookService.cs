using BSApp.Entities.Dtos;
using BSApp.Entities.Models;

namespace BSApp.Service.Contracts;

public interface IBookService
{
    IEnumerable<BookDto> GetAllBooks(bool trackChanges);
    BookDto GetBookById(int id, bool trackChanges);
    BookDto CreateBook(Book book);
    void UpdateBook(int id, UpdateBookDto bookDto, bool trackChanges);
    void DeleteBook(int id, bool trackChanges);
}
