using CleanAPI.Application.Features.Beers.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BeersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetBeersListQueryVm>> Get()
        {
            var result = await _mediator.Send(new GetBeersListQuery());
            return Ok(result);
        }
    }
}
