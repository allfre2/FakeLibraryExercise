using Application.Commands;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class LoansController : FakeLibraryBaseController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetLoans([FromQuery] LoansQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddLoan([FromBody] AddLoanCommand command)
    {
        return Created(HttpContext.Request.Path, await Mediator.Send(command));
    }

    [HttpPut("{Id}")]
    [Authorize]
    public async Task<IActionResult> EditLoan([FromRoute] int Id, [FromBody] EditLoanCommand command)
    {
        command.Id = Id;

        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{Id}")]
    [Authorize]
    public async Task<IActionResult> DeleteLoan([FromRoute] DeleteLoanCommand command)
    {
        await Mediator.Send(command);

        return Ok();
    }
}
