using System.Text.Json;
using BSApp.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BSApp.Presentation.Controllers;

[ApiVersion("2.0")]
[ApiExplorerSettings(GroupName = "v2")]
[ApiController]
[Route("api/v{v:apiversion}/books")]
public class BooksV2Controller : ControllerBase
{
    private readonly IServiceManager _manager;

    public BooksV2Controller(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpHead]
    [HttpGet]
    public async Task<IActionResult> GetAllBooks(bool trackChanges)
    {
        var books = await _manager.BookService.GetAllBooksAsync(trackChanges);
        // Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(books));
        return Ok(books);

    }


}
