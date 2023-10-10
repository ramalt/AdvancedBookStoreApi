using BSApp.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using BSApp.Entities.Exceptions;
using BSApp.Entities.Dtos;
using BSApp.Presentation.ActionFilters;
using BSApp.Entities.RequestFeatures;
using System.Text.Json;

namespace BSApp.Presentation.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(LogFilterAttribute))]
public class BooksController : ControllerBase
{
    private readonly IServiceManager _manager;

    public BooksController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneBook([FromRoute(Name = "id")] int id)
    {

        var book = await _manager.BookService.GetBookByIdAsync(id, false);

        if (book is null)
            throw new BookNotFoundException(id);

        return Ok(book);

    }

    [HttpHead]
    [HttpGet]
    public async Task<IActionResult> GetAllBooks([FromQuery]BookParameters param)
    {
        var pagedBooks = await _manager.BookService.GetAllBooksAsync(param, false);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedBooks.metaData));
        return Ok(pagedBooks);

    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook([FromRoute(Name = "id")] int id, [FromBody] UpdateBookDto bookDto)
    {
        await _manager.BookService.UpdateBookAsync(id, bookDto, true);

        return NoContent();
    }

    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto bookDto)
    {

        await _manager.BookService.CreateBookAsync(bookDto);

        return StatusCode(201, bookDto);

    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook([FromRoute(Name = "id")] int id)
    {

        await _manager.BookService.DeleteBookAsync(id, false);
        return NoContent();

    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PartiallyUpdateBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<UpdateBookDto> bookDto)
    {
        if(bookDto is null)
            return BadRequest();

        var entity =await _manager.BookService.PartialUpdateBookAsync(id, false);

        bookDto.ApplyTo(entity.updateBookDto, ModelState);

        TryValidateModel(entity.updateBookDto);
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        
        await _manager.BookService.SaveChangesForPatchAsync(entity.updateBookDto, entity.book);

        return NoContent();

    }

    [HttpOptions]
    public IActionResult GetBookOptions()
    {
        Response.Headers.Add("allow", "POST,GET,PUT,DELETE,PATHC,HEAD,OPTIONS");
        return Ok();
    }

}
