using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BSApp.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RootController : ControllerBase
{
    private readonly LinkGenerator _linkGenerator;

    public RootController(LinkGenerator linkGenerator)
    {
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name = "GetRoot")]
    public async Task<IActionResult> GetRoot([FromHeader( Name = "accept")] string mediaType)
    {   
        if (mediaType.Contains("application/vnd.customtype.root"))
        {
            //TODO: make hateoas implementation and return a Link list here.
            return Ok();
        }

        return NoContent();
    }
}
