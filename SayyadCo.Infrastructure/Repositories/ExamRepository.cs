using Microsoft.EntityFrameworkCore;
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

        protected override IQueryable<Exam> GetByIdQueryable()
            => _dbSet
            .Where(e => e.Status == ExamStatus.Approved)
            .Include(e => e.Questions);
    }
}
