using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;
using SayyadCo.Infrastructure.Identity;

namespace SayyadCo.Infrastructure.Repositories
{
    public class UserGameRoleRepository : IUserGameRoleRepository
    {
        private readonly AppDbContext _context;

        public UserGameRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AssignRoleToUserAsync(string userId, string gameRoleId, string sectionId, string gameId)
        {
            var entity = new UserGameRole
            {
                UserId = userId,
                GameRoleId = gameRoleId,
                SectionId = sectionId,
                GameId = gameId
            };

            await _context.UserGameRoles.AddAsync(entity);
        }

        public async Task RemoveRoleFromUserAsync(string userId, string gameRoleId, string sectionId, string gameId)
        {
            var entity = await _context.UserGameRoles.FindAsync(userId, gameRoleId, sectionId, gameId);

            if (entity != null)
                _context.UserGameRoles.Remove(entity);
        }

        public async Task<bool> UserHasRoleAsync(string userId, string gameRoleId, string sectionId, string gameId)
        {
            return await _context.UserGameRoles
                .AnyAsync(x =>
                    x.UserId == userId &&
                    x.GameRoleId == gameRoleId &&
                    x.SectionId == sectionId &&
                    x.GameId == gameId);
        }

        public async Task<string?> GetUserRoleAsync(string userId, string sectionId, string gameId)
        {
            return await _context.UserGameRoles
                .Where(x => x.UserId == userId &&
                            x.SectionId == sectionId &&
                            x.GameId == gameId)
                .Select(x => x.GameRoleId)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<TeacherGame>> GetTeacherGamesAsync(string userId, QueryParameters parameters)
        {
            var query = _context.TeacherGames
                .Include(t => t.SectionGame)
                    .ThenInclude(sg => sg.Section)
                .Include(t => t.SectionGame)
                    .ThenInclude(sg => sg.Game)
                .AsNoTracking()
                .Where(t => t.UserId == userId);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<TeacherGame>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }
    }
}
