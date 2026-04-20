using Microsoft.AspNetCore.Authorization;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces;
using System.Security.Claims;
using System.Text.Json;

namespace SayyadCo.API.Authorization
{
    public class GameRoleAuthorizationHandler : AuthorizationHandler<GameRoleRequirement>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GameRoleAuthorizationHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GameRoleRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null)
            {
                context.Fail();
                return;
            }

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Fail();
                return;
            }

            if (context.User.IsInRole(AppRoles.SuperAdmin) || context.User.IsInRole(AppRoles.Admin))
            {
                context.Succeed(requirement);
                return;
            }

            httpContext.Request.EnableBuffering();

            string body;

            using (var reader = new StreamReader(httpContext.Request.Body, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
            }

            httpContext.Request.Body.Position = 0;

            using var doc = JsonDocument.Parse(body);

            var root = doc.RootElement;

            var gameId = root.GetProperty("gameId").GetString();
            var sectionId = root.GetProperty("sectionId").GetString();

            if (string.IsNullOrEmpty(sectionId) || string.IsNullOrEmpty(gameId))
            {
                context.Fail();
                return;
            }

            foreach (var role in requirement.AllowedRoles)
            {
                if (await _unitOfWork.UserGameRoles.UserHasRoleAsync(userId, role, sectionId, gameId))
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            context.Fail();
        }
    }
}
