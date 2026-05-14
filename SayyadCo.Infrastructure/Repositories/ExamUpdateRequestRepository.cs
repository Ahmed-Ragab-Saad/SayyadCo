using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class ExamUpdateRequestRepository : Repository<ExamUpdateRequest>, IExamUpdateRequestRepository
    {
        public ExamUpdateRequestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ExamUpdateRequest?> GetPendingByExamIdAsync(string examId)
            => await _context.ExamUpdateRequests
                .AsNoTracking()
                .FirstOrDefaultAsync(r =>
                    r.ExamId == examId &&
                    r.Status == RequestStatus.Pending);

        // ExamUpdateRequestRepository.cs
        public async Task<PagedResult<ExamUpdateRequest>> GetPendingUpdatesAsync(QueryParameters parameters)
        {
            var query = _context.ExamUpdateRequests
                .Include(r => r.Exam)
                .AsNoTracking()
                .Where(r => r.Status == RequestStatus.Pending);

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var search = parameters.SearchTerm.ToLower();
                query = query.Where(r =>
                    r.TitleAr.ToLower().Contains(search) ||
                    r.TitleEn.ToLower().Contains(search));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResult<ExamUpdateRequest>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }
    }
}
