using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Base;

[ApiController]
public class FakeLibraryBaseController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator
        => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
