using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
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

        public async Task<PagedResult<StudentGame>> GetStudentGamesAsync(string userId, QueryParameters parameters)
        {
            var query = _context.StudentGames
                .Include(s => s.SectionGame)
                    .ThenInclude(sg => sg.Section)
                .Include(s => s.SectionGame)
                    .ThenInclude(sg => sg.Game)
                .AsNoTracking()
                .Where(s => s.UserId == userId);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<StudentGame>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedResult<FunnyGameDto>> GetFunnyGamesAsync(string userId, QueryParameters parameters)
        {
            var teacherGames = _context.TeacherGames
                .Include(t => t.SectionGame)
                    .ThenInclude(sg => sg.Section)
                .Include(t => t.SectionGame)
                    .ThenInclude(sg => sg.Game)
                .AsNoTracking()
                .Where(t => t.UserId == userId &&
                            t.SectionGame.Section.SectionType == SectionType.Funny)
                .Select(t => new FunnyGameDto
                {
                    SectionId = t.SectionId,
                    SectionTitleEn = t.SectionGame.Section.TitleEn,
                    SectionTitleAr = t.SectionGame.Section.TitleAr,
                    GameId = t.GameId,
                    GameTitleEn = t.SectionGame.Game.TitleEn,
                    GameTitleAr = t.SectionGame.Game.TitleAr,
                    GameImage = t.SectionGame.Game.Image,
                    Role = "Teacher",
                    ExpirationDate = t.ExpirationDate
                });

            var studentGames = _context.StudentGames
                .Include(s => s.SectionGame)
                    .ThenInclude(sg => sg.Section)
                .Include(s => s.SectionGame)
                    .ThenInclude(sg => sg.Game)
                .AsNoTracking()
                .Where(s => s.UserId == userId &&
                            s.SectionGame.Section.SectionType == SectionType.Funny)
                .Select(s => new FunnyGameDto
                {
                    SectionId = s.SectionId,
                    SectionTitleEn = s.SectionGame.Section.TitleEn,
                    SectionTitleAr = s.SectionGame.Section.TitleAr,
                    GameId = s.GameId,
                    GameTitleEn = s.SectionGame.Game.TitleEn,
                    GameTitleAr = s.SectionGame.Game.TitleAr,
                    GameImage = s.SectionGame.Game.Image,
                    Role = "Student",
                    ExpirationDate = s.ExpirationDate
                });

            var combined = teacherGames.Union(studentGames);

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var search = parameters.SearchTerm.ToLower();
                combined = combined.Where(g =>
                    g.GameTitleEn.ToLower().Contains(search) ||
                    g.GameTitleAr.ToLower().Contains(search) ||
                    g.SectionTitleEn.ToLower().Contains(search));
            }

            var totalCount = await combined.CountAsync();
            var items = await combined
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<FunnyGameDto>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }
    }
}
