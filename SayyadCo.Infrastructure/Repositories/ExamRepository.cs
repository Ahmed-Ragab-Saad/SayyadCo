using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class ExamRepository : Repository<Exam>, IExamRepository
    {
        public ExamRepository(AppDbContext context) : base(context)
        {
        }

        public async Task DeleteWithQuestionsAsync(string id)
        {
            var exam = await _dbSet
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exam is not null)
            {
                _context.Questions.RemoveRange(exam.Questions);
                _context.Exams.Remove(exam);
            }
        }

        public async Task<PagedResult<Exam>> GetPendingExamsAsync(QueryParameters parameters)
        {
            var query = _dbSet
                .Include(e => e.Questions)
                .AsNoTracking()
                .Where(e => e.Status == ExamStatus.Pending);

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var search = parameters.SearchTerm.ToLower();
                query = query.Where(e =>
                    e.TitleAr.ToLower().Contains(search) ||
                    e.TitleEn.ToLower().Contains(search));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<Exam>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }

        protected override IQueryable<Exam> GetByIdQueryable()
            => _dbSet
            .Where(e => e.Status == ExamStatus.Approved)
            .Include(e => e.Questions);
    }
}
