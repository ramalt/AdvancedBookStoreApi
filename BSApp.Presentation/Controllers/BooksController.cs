using BSApp.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using BSApp.Entities.Exceptions;
using BSApp.Entities.Dtos;

namespace BSApp.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IServiceManager _manager;

    public BooksController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
    {

        var book = _manager.BookService.GetBookById(id, false);

        if (book is null)
            throw new BookNotFoundException(id);

        return Ok(book);

    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var books = _manager.BookService.GetAllBooks(false);
        return Ok(books);

    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] UpdateBookDto bookDto)
    {

        if (bookDto is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        _manager.BookService.UpdateBook(id, bookDto, true);

        return NoContent();
    }

    [HttpPost]
    public IActionResult CreateBook([FromBody] CreateBookDto bookDto)
    {

        if (bookDto is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        

        _manager.BookService.CreateBook(bookDto);

        return StatusCode(201, bookDto);

    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
    {

        _manager.BookService.DeleteBook(id, false);
        return NoContent();

    }

    [HttpPatch("{id:int}")]
    public IActionResult PartiallyUpdateBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<UpdateBookDto> bookDto)
    {
        if(bookDto is null)
            return BadRequest();

        var entity = _manager.BookService.PartialUpdateBook(id, false);

        bookDto.ApplyTo(entity.updateBookDto, ModelState);

        TryValidateModel(entity.updateBookDto);
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        _manager.BookService.SaveChangesForPatch(entity.updateBookDto, entity.book);

        return NoContent();

    }


}
