using APBD_Tutorial9.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Tutorial9.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IMediator _mediator;

    public WarehouseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductToWirehouse([FromBody] AddProductToWarehouseCommand command)
    {
        var result = await _mediator.Send(command);
        return result.success ? Ok(new {message = result.message, id = result.resultId}) : BadRequest(new {error = result.message});
    }
}