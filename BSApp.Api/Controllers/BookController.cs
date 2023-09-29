using Azure;
using BSApp.Entities.Models;
using BSApp.Service.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BSApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IServiceManager _manager;

    public BookController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
    {
        try
        {
            var book = _manager.BookService.GetBookById(id, false);

            if (book is null)
                return NotFound($"book not found with id: {id}");
            return Ok(book);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        try
        {
            var books = _manager.BookService.GetAllBooks(false);
            return Ok(books);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
    {
        try
        {
            if (book is null)
                return BadRequest();

            _manager.BookService.UpdateBook(id, book, true);

            return NoContent();
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateBook([FromBody] Book book)
    {
        try
        {
            if (book is null)
                return BadRequest();

            _manager.BookService.CreateBook(book);

            return StatusCode(201, book);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
    {
        try
        {
            _manager.BookService.DeleteBook(id, false);
            return NoContent();
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("{id:int}")]
    public IActionResult PartiallyUpdateBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> book)
    {
        try
        {
            var existingBook = _manager.BookService.GetBookById(id, false);
            if (existingBook is null)
                return NotFound();

            book.ApplyTo(existingBook);
            _manager.BookService.UpdateBook(id, existingBook, true);

            return NoContent();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
