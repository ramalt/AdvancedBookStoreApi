using AutoMapper;
using BSApp.Entities.Dtos;
using BSApp.Entities.Models;
using BSApp.Repository.Data;
using BSApp.Service.Contracts;

namespace BSApp.Service;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;

    public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
    {
        _manager = manager;
        _logger = logger;
        _mapper = mapper;
    }

    public BookDto CreateBook(Book book)
    {

        _manager.Book.CreateOneBook(book);
        _manager.Save();

        var createdBook = _mapper.Map<BookDto>(book);

        return createdBook;
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

    public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
    {

        var books =  _manager.Book.GetAllBooks(trackChanges);

        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public BookDto GetBookById(int id, bool trackChanges)
    {
        var book =  _manager.Book.GetOneBookById(id, trackChanges);
        var mapped = _mapper.Map<BookDto>(book);
        return mapped;
    }

    public void UpdateBook(int id, UpdateBookDto bookDto, bool trackChanges)
    {
        var existingBook = _manager.Book.GetOneBookById(id, trackChanges);

        if (existingBook is null)
        {
            var msg = $"Book not found with id : {id}";
            _logger.LogInfo(msg);
            throw new Exception(msg);
        }

        existingBook = _mapper.Map<Book>(bookDto);

        _manager.Book.Update(existingBook);
        _manager.Save();
    }
}
