using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Test?> GetByIdWithQuestionsAsync(string id)
            => await _dbSet
                .Include(t => t.Questions)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<PagedResult<Test>> GetBySectionGameAsync(string sectionId, string gameId, string academicYearId,
            Semester semester, QueryParameters parameters)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(t =>
                    t.SectionId == sectionId &&
                    t.GameId == gameId &&
                    t.AcademicYearId == academicYearId &&
                    t.Semester == semester);

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var search = parameters.SearchTerm.ToLower();
                query = query.Where(t =>
                    t.TitleAr.ToLower().Contains(search) ||
                    t.TitleEn.ToLower().Contains(search));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<Test>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }

        protected override IQueryable<Test> GetByIdQueryable()
            => _dbSet.Include(t => t.Questions);
    }
}
