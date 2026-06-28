using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class UserGameRepository : Repository<UserGame>, IUserGameRepository
    {
        public UserGameRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserGame?> GetAsync(string userId, string sectionId, string gameId)
            => await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(t =>
                    t.UserId == userId &&
                    t.SectionId == sectionId &&
                    t.GameId == gameId);

        public async Task<string?> GetUserRoleAsync(string userId, string sectionId, string gameId)
            => await _dbSet.AsNoTracking()
                .Include(ug => ug.GameRole)
                .Where(ug => ug.SectionId == sectionId && ug.GameId == gameId && ug.UserId == userId)
                .Select(ug => ug.GameRole.Role)
                .FirstOrDefaultAsync();

        public async Task<PagedResult<UserGame>> GetTeacherGamesAsync(string userId, QueryParameters parameters)
            => await GtUserGamesByRole(userId, GameRoles.Teacher, parameters);

        public async Task<PagedResult<UserGame>> GetStudentGamesAsync(string userId, QueryParameters parameters)
            => await GtUserGamesByRole(userId, GameRoles.Student, parameters);

        public async Task<PagedResult<FunnyGameDto>> GetFunnyGamesAsync(string userId, QueryParameters parameters)
        {
            var games = _dbSet
                .Include(ug => ug.SectionGame)
                    .ThenInclude(sg => sg.Section)
                .Include(ug => ug.SectionGame)
                    .ThenInclude(sg => sg.Game)
                .Include(ug => ug.GameRole)
                .AsNoTracking()
                .Where(t => t.UserId == userId && t.SectionGame.Section.SectionType == SectionType.Funny)
                .Select(ug => new FunnyGameDto
                {
                    SectionId = ug.SectionId,
                    SectionTitleEn = ug.SectionGame.Section.TitleEn,
                    SectionTitleAr = ug.SectionGame.Section.TitleAr,
                    GameId = ug.GameId,
                    GameTitleEn = ug.SectionGame.Game.TitleEn,
                    GameTitleAr = ug.SectionGame.Game.TitleAr,
                    GameImage = ug.SectionGame.Game.Image,
                    Role = ug.GameRole.Role,
                    ExpirationDate = ug.ExpirationDate
                });

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var search = parameters.SearchTerm.ToLower();
                games = games.Where(g =>
                    g.GameTitleEn.ToLower().Contains(search) ||
                    g.GameTitleAr.ToLower().Contains(search) ||
                    g.SectionTitleEn.ToLower().Contains(search));
            }

            var totalCount = await games.CountAsync();
            var items = await games
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<FunnyGameDto>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }

        private async Task<PagedResult<UserGame>> GtUserGamesByRole(string userId, string role, QueryParameters parameters)
        {
            var query = _dbSet
                .Include(ug => ug.SectionGame)
                    .ThenInclude(sg => sg.Section)
                .Include(ug => ug.SectionGame)
                    .ThenInclude(sg => sg.Game)
                .Include(ug => ug.GameRole)
                .AsNoTracking()
                .Where(ug => ug.UserId == userId && ug.GameRole.Role == GameRoles.Teacher);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<UserGame>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }
    }
}
