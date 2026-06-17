using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Plans.Commands.CreatePlan;
using SayyadCo.Domain.Common;

namespace SayyadCo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
    public class PlansController : CustomBaseController
    {
        /// <summary>Create a new plan</summary>
        /// <remarks>
        /// Creates a new subscription plan. Only SuperAdmins can create plans.
        ///
        /// Sample request:
        ///
        ///     POST /api/plans
        ///     {
        ///         "titleAr": "الخطة الأساسية",
        ///         "titleEn": "Basic Plan",
        ///         "descriptionAr": "خطة أساسية للطلاب",
        ///         "descriptionEn": "Basic plan for students",
        ///         "durationInDays": 30,
        ///         "price": 49.99
        ///     }
        /// </remarks>
        /// <response code="200">Plan created successfully</response>
        /// <response code="400">Validation error or name already exists</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePlanResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create([FromBody] CreatePlanCommand command)
            => HandleResult(await Mediator.Send(command));
    }
}
