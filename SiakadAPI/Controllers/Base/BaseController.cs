using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using System.Threading.Tasks;

namespace SiakadAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<TCreateCommand, TUpdateCommand, TUpdate> : ControllerBase
        where TCreateCommand : IRequest<int>
        where TUpdateCommand : IRequest<int>
        where TUpdate : IRequest<int>
    {
        protected readonly ILogger _logger;
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;


        public BaseController(ILogger logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        private async Task<IActionResult> SendCommand(IRequest<int> command)
        {
            var result = await _mediator.Send(command);

            if (result == 0) { new BadRequestResult(); }

            return new OkResult();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [AllowAnonymous]
        public virtual async Task<IActionResult> Create(TCreateCommand command)
        {
            return await SendCommand(command);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [AllowAnonymous]
        public virtual async Task<IActionResult> Update(TUpdateCommand command)
        {
            return await SendCommand(command);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [AllowAnonymous]
        public virtual async Task<IActionResult> Delete(TUpdateCommand command)
        {
            return await SendCommand(command);
        }
    }
}