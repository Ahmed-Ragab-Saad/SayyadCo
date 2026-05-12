using Microsoft.EntityFrameworkCore;
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
    }
}
