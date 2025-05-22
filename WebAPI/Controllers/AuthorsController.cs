using Application.Commands;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthorsController : FakeLibraryBaseController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAuthors([FromQuery] AuthorsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddAuthor(AddAuthorCommand command)
    {
        return Created(HttpContext.Request.Path, await Mediator.Send(command));
    }
}
