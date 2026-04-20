using MediatR;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected async Task<IActionResult> Send<TResponse>(IRequest<TResponse> request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            return result.Status switch
            {
                ResultStatus.Success => Ok(result.Data),
                ResultStatus.NotFound => NotFound(new ErrorResponse { Error = result.Error }),
                ResultStatus.Unauthorized => Unauthorized(new ErrorResponse { Error = result.Error }),
                ResultStatus.Forbidden => StatusCode(403, new ErrorResponse { Error = result.Error }),
                ResultStatus.ValidationError => BadRequest(result),
                ResultStatus.UnverifiedEmail => StatusCode(403, new
                {
                    Error = result.Error,
                    VerificationToken = result.VerificationToken ?? string.Empty
                }),
                _ => BadRequest(new ErrorResponse { Error = result.Error })
            };
        }

    }
}
