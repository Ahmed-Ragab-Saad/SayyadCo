using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Interfaces.Repositories;

namespace SayyadCo.Infrastructure.Services.GameAccess
{
    public class GameAccessService : IGameAccessService
    {
        private readonly IUserGameRepository _userGameRepository;
        private readonly IAuthService _authService;

        public GameAccessService(IUserGameRepository userGameRepository, IAuthService authService)
        {
            _userGameRepository = userGameRepository;
            _authService = authService;
        }

        public async Task<GameAccessResult> CheckAccessAsync(string userId, string sectionId, string gameId)
        {
            var roles = await _authService.GetRolesAsync(userId);
            var isAdminOrSuperAdmin = roles.Any(r => r == AppRoles.SuperAdmin || r == AppRoles.Admin);

            if (isAdminOrSuperAdmin)
                return new GameAccessResult
                {
                    HasAccess = true,
                    IsAdminOrSuperAdmin = true
                };

            var userRole = await _userGameRepository.GetUserRoleAsync(userId, sectionId, gameId);

            if (userRole == GameRoles.Teacher)
                return new GameAccessResult
                {
                    HasAccess = true,
                    IsTeacher = true
                };

            if (userRole == GameRoles.Student)
                return new GameAccessResult
                {
                    HasAccess = true,
                    IsStudent = true
                };

            return new GameAccessResult { HasAccess = false };
        }
    }
}
