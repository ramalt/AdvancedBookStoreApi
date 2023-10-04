using System.Dynamic;
using BSApp.Entities.Dtos;
using BSApp.Entities.Models;
using BSApp.Entities.RequestFeatures;

namespace BSApp.Service.Contracts;

public interface IBookService
{
    Task<(IEnumerable<ExpandoObject> books, MetaData metaData)> GetAllBooksAsync(BookParameters param, bool trackChanges);
    Task<BookDto> GetBookByIdAsync(int id, bool trackChanges);
    Task<BookDto> CreateBookAsync(CreateBookDto bookDto);
    Task UpdateBookAsync(int id, UpdateBookDto bookDto, bool trackChanges);
    Task DeleteBookAsync(int id, bool trackChanges);
    Task<(UpdateBookDto updateBookDto, Book book)> PartialUpdateBookAsync(int id, bool trackChanges);
    Task SaveChangesForPatchAsync(UpdateBookDto updateBookDto,Book book);
}
