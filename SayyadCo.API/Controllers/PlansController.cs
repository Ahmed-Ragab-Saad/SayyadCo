using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Plans.Commands.CreatePlan;
using SayyadCo.Application.Features.Plans.Commands.DeletePlan;
using SayyadCo.Application.Features.Plans.Commands.UpdatePlan;
using SayyadCo.Application.Features.Plans.Queries.GetAllPlans;
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

        /// <summary>Update a plan</summary>
        /// <remarks>
        /// Updates an existing subscription plan. Only SuperAdmins can update plans.
        ///
        /// Sample request:
        ///
        ///     PUT /api/plans/{id}
        ///     {
        ///         "titleAr": "الخطة الأساسية المحدثة",
        ///         "titleEn": "Updated Basic Plan",
        ///         "descriptionAr": "خطة أساسية محدثة للطلاب",
        ///         "descriptionEn": "Updated basic plan for students",
        ///         "durationInDays": 60,
        ///         "price": 79.99
        ///     }
        /// </remarks>
        /// <response code="200">Plan updated successfully</response>
        /// <response code="400">Validation error or duplicate plan data</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Plan not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdatePlanResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePlanCommand command)
        {
            command.Id = id;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>Delete a plan</summary>
        /// <remarks>
        /// Deletes an existing subscription plan. Only SuperAdmins can delete plans.
        ///
        /// Sample request:
        ///
        ///     DELETE /api/plans/{id}
        /// </remarks>
        /// <response code="200">Plan deleted successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Plan not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
            => HandleResult(await Mediator.Send(new DeletePlanCommand { Id = id }));

        /// <summary>Get all plans</summary>
        /// <remarks>
        /// Retrieves a paginated list of all subscription plans.
        ///
        /// Sample request:
        ///
        ///     GET /api/plans?pageNumber=1&pageSize=10
        /// </remarks>
        /// <response code="200">Plans retrieved successfully</response>
        /// <response code="400">Invalid query parameters</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResult<GetAllPlansResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPlansQuery query)
            => HandleResult(await Mediator.Send(query));
    }
}
