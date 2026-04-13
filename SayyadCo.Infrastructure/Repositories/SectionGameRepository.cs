using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class SectionGameRepository : ISectionGameRepository
    {
        private readonly AppDbContext _context;

        public SectionGameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SectionGame sectionGame)
            => await _context.SectionGames.AddAsync(sectionGame);

        public async Task<SectionGame?> GetAsync(string sectionId, string gameId)
            => await _context.SectionGames
                .Include(sg => sg.Section)
                .Include(sg => sg.Game)
                .AsNoTracking()
                .FirstOrDefaultAsync(sg => sg.SectionId == sectionId && sg.GameId == gameId);

        public async Task<PagedResult<SectionGame>> GetBySectionIdAsync(string sectionId, QueryParameters parameters)
        {
            var query = _context.SectionGames
                .Include(sg => sg.Game)
                .AsNoTracking()
                .Where(sg => sg.SectionId == sectionId);

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var search = parameters.SearchTerm.ToLower();
                query = query.Where(sg =>
                    sg.Game.TitleAr.ToLower().Contains(search) ||
                    sg.Game.TitleEn.ToLower().Contains(search));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<SectionGame>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }

        public void Remove(SectionGame sectionGame)
            => _context.SectionGames.Remove(sectionGame);
    }
}
