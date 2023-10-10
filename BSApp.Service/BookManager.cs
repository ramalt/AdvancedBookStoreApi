using System.Dynamic;
using AutoMapper;
using BSApp.Entities.Dtos;
using BSApp.Entities.Exceptions;
using BSApp.Entities.Models;
using BSApp.Entities.RequestFeatures;
using BSApp.Repository.Data;
using BSApp.Service.Contracts;

namespace BSApp.Service;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;
    private readonly IDataShaper<BookDto> _shaper;

    public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IDataShaper<BookDto> shaper)
    {
        _manager = manager;
        _logger = logger;
        _mapper = mapper;
        _shaper = shaper;
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
        var book = await GetBookOneBookAndCheckExist(id, trackChanges);
        _manager.Book.Delete(book);
        await _manager.SaveAsync();

    }

    public async Task<(IEnumerable<ExpandoObject> books, MetaData metaData)> GetAllBooksAsync(BookParameters param,bool trackChanges)
    {
        if (!param.ValidPriceRange)
            throw new PriceOutOfRangeBadRequest();

        var books = await _manager.Book.GetAllBooksAsync(param,trackChanges);

        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

        var shaped = _shaper.ShapeData(booksDto, param.Fields);
        return (shaped, books.MetaData);
    }

    public async Task<List<BookDto>> GetAllBooksAsync(bool trackChanges)
    {
        var books = await _manager.Book.GetAllBooksAsync(trackChanges);

        return _mapper.Map<List<BookDto>>(books);

    }

    public async Task<BookDto> GetBookByIdAsync(int id, bool trackChanges)
    {
        var book =await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
        return _mapper.Map<BookDto>(book);

    }

    public async Task<(UpdateBookDto updateBookDto, Book book)> PartialUpdateBookAsync(int id, bool trackChanges)
    {
        var book = await GetBookOneBookAndCheckExist(id, trackChanges);
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
        var book = await GetBookOneBookAndCheckExist(id, trackChanges);
        book = _mapper.Map<Book>(bookDto);
        _manager.Book.Update(book);
        await _manager.SaveAsync();
    }

    private async Task<Book> GetBookOneBookAndCheckExist(int id,bool trackChanges){
        var book = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);
        if(book is null)
            throw new BookNotFoundException(id);
        return book;
    }
}
