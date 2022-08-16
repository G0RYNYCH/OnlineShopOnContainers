using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public BaseController(IMediator mediator) => Mediator = mediator;

        protected IMediator Mediator { get; }
    }
}
