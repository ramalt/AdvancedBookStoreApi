using AutoMapper;
using BSApp.Entities.Dtos;
using BSApp.Entities.Exceptions;
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

    public async Task<BookDto> CreateBookAsync(CreateBookDto bookDto)
    {
        Book book = _mapper.Map<Book>(bookDto);
        _manager.Book.CreateOneBook(book);
        await _manager.SaveAsync();

        return _mapper.Map<BookDto>(bookDto);
    }

    public async Task DeleteBookAsync(int id, bool trackChanges)
    {
        var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
        if (book is null)
        {
            var msg = $"Book not found with id : {id}";
            _logger.LogInfo(msg);
            throw new Exception(msg);
        }


        _manager.Book.Delete(book);
        await _manager.SaveAsync();

    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
    {

        var books = await _manager.Book.GetAllBooksAsync(trackChanges);

        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto> GetBookByIdAsync(int id, bool trackChanges)
    {
        var book =await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
        return _mapper.Map<BookDto>(book);

    }

    public async Task<(UpdateBookDto updateBookDto, Book book)> PartialUpdateBookAsync(int id, bool trackChanges)
    {
        var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
        if (book is null)
            throw new BookNotFoundException(id);
        
        var updateBookDto = _mapper.Map<UpdateBookDto>(book);

        return (updateBookDto, book);
    }

    public async Task SaveChangesForPatchAsync(UpdateBookDto updateBookDto,Book book)
    {
        _mapper.Map(updateBookDto, book);
        await _manager.SaveAsync();
    }

    public async Task UpdateBookAsync(int id, UpdateBookDto bookDto, bool trackChanges)
    {
        var existingBook = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);

        if (existingBook is null)
        {
            var msg = $"Book not found with id : {id}";
            _logger.LogInfo(msg);
            throw new BookNotFoundException(id);
        }

        existingBook = _mapper.Map<Book>(bookDto);

        _manager.Book.Update(existingBook);
        await _manager.SaveAsync();
    }
}
