using Application.Commands;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BooksController : FakeLibraryBaseController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetBooks([FromQuery] BooksQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddBook(AddBookCommand command)
    {
        return Created(HttpContext.Request.Path, await Mediator.Send(command));
    }
}
