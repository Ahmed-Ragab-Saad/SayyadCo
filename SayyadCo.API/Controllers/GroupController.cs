using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Groups.Commands.CreateGroup;
using SayyadCo.Application.Features.Groups.Commands.DeleteGroup;
using SayyadCo.Application.Features.Groups.Commands.UpdateGroup;
using SayyadCo.Application.Features.Groups.Queries;
using SayyadCo.Application.Features.Groups.Queries.GetGroupById;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.API.Controllers
{
    [Route("api/sections/{sectionId}/games/{gameId}/academic-years/{academicYearId}/semesters/{semester}")]
    [ApiController]
    public class GroupController : CustomBaseController
    {
        /// <summary>Create a new group</summary>
        /// <remarks>
        /// Creates a new group inside a SectionGame for a specific academic year and semester.
        /// Each Teacher can have only one group per SectionGame per academic year per semester.
        /// Admins and SuperAdmins can create multiple groups.
        ///
        /// Sample request:
        ///
        ///     POST /api/sections/3fa85f64.../games/4gb96g75.../academic-years/5cd12h89.../semesters/1/groups
        ///     {
        ///         "name": "Math Group A",
        ///         "description": "Group for Math students",
        ///         "image": "https://res.cloudinary.com/sayyadco/image/upload/group.jpg",
        ///         "isPrivate": true,
        ///         "password": "123456"
        ///     }
        /// </remarks>
        /// <response code="200">Group created successfully</response>
        /// <response code="400">Validation error or Teacher already has a group</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">SectionGame or AcademicYear not found</response>
        [HttpPost("groups")]
        [ProducesResponseType(typeof(CreateGroupResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(string sectionId, string gameId, string academicYearId, Semester semester,
            [FromBody] CreateGroupCommand command)
        {
            command.SectionId = sectionId;
            command.GameId = gameId;
            command.AcademicYearId = academicYearId;
            command.Semester = semester;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>Update an existing group</summary>
        /// <remarks>
        /// Updates group details inside a SectionGame.
        /// 
        /// Only the group owner (Teacher) or Admins/SuperAdmins can update the group.
        /// You can modify basic information such as name, description, image, and privacy settings.
        /// 
        /// Sample request:
        /// 
        ///     PUT /api/sections/3fa85f64.../games/4gb96g75.../academic-years/5cd12h89.../semesters/1/groups/c6ax41mu...
        ///     {
        ///         "name": "Updated Group Name",
        ///         "description": "Updated description",
        ///         "image": "https://res.cloudinary.com/sayyadco/image/upload/group.jpg",
        ///         "isPrivate": false,
        ///         "password": null
        ///     }
        /// </remarks>
        /// <response code="200">Group updated successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Group not found</response>
        [HttpPut("groups/{groupId}")]
        [ProducesResponseType(typeof(UpdateGroupResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string sectionId, string gameId, string academicYearId, Semester semester,
            string groupId, [FromBody] UpdateGroupCommand command)
        {
            command.SectionId = sectionId;
            command.GameId = gameId;
            command.AcademicYearId = academicYearId;
            command.Semester = semester;
            command.GroupId = groupId;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>Delete an existing group</summary>
        /// <remarks>
        /// Deletes a group from a SectionGame for a specific academic year and semester.
        /// 
        /// Only the group owner (Teacher) or Admins/SuperAdmins can delete the group.
        /// This action is irreversible and will remove all related data associated with the group.
        /// 
        /// Sample request:
        /// 
        ///     DELETE /api/sections/sy4c21m8.../games/4gb96g75.../academic-years/5cd12h89.../semesters/1/groups/9hf73k2a...
        /// </remarks>
        /// <response code="200">Group deleted successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Group not found</response>
        [HttpDelete("groups/{groupId}")]
        [Authorize(Roles = $"{AppRoles.SuperAdmin},{AppRoles.Admin}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string sectionId, string gameId, string academicYearId,
            Semester semester, string groupId)
            => HandleResult(await Mediator.Send(new DeleteGroupCommand
            {
                SectionId = sectionId,
                GameId = gameId,
                AcademicYearId = academicYearId,
                Semester = semester,
                GroupId = groupId
            }));

        /// <summary>Get all groups from a game inside a section in specific academic year and semester</summary>
        /// <remarks>
        /// Get all groups from a game inside a section in specific academic year and semester
        /// Admins, Super Admins, Teacher, And Students can access this
        /// 
        /// Sample request:
        ///
        ///     GET /api/sections/3fa85f64.../games/4gb96g75.../academic-years/5cd12h89.../semesters/1/groups?pageNumber=1&amp;pageSize=10&amp;searchTerm=million&amp;orderBy=titleEn&amp;isDescending=false
        ///     
        /// </remarks>
        /// <response code="200">Returns paginated list of groups</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        [HttpGet("groups")]
        [ProducesResponseType(typeof(GetAllGroupsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll(string sectionId, string gameId, string academicYearId, Semester semester,
            [FromQuery] GetAllGroupsQuery command)
        {
            command.SectionId = sectionId;
            command.GameId = gameId;
            command.AcademicYearId = academicYearId;
            command.Semester = semester;
            return HandleResult(await Mediator.Send(command));
        }

        /// <summary>Get group by ID with approved exams</summary>
        /// <remarks>
        /// Retrieves a specific group within a SectionGame.
        /// 
        /// Returns the group details along with its exams that have status "Approved" only.
        /// Exams with other statuses (e.g., Pending or Rejected) are excluded from the response.
        /// 
        /// If the group has no approved exams, the group is still returned with an empty Exams collection.
        /// 
        /// Sample request:
        /// 
        ///     GET /api/sections/sy4c21m8.../games/4gb96g75.../groups/9hf73k2a...
        /// </remarks>
        /// <response code="200">Group retrieved successfully</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Group not found</response>
        [HttpGet("groups/{groupId}")]
        [ProducesResponseType(typeof(GetGroupByIdResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string sectionId, string gameId, string academicYearId,
            Semester semester, string groupId)
            => HandleResult(await Mediator.Send(new GetGroupByIdQuery
            {
                SectionId = sectionId,
                GameId = gameId,
                AcademicYearId = academicYearId,
                Semester = semester,
                GroupId = groupId
            }));
    }
}
