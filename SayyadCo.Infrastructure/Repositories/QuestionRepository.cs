using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddQuestions(IList<Question> questionList)
            => await _context.Questions.AddRangeAsync(questionList);

        public async Task<int> GetQuestionsCount(string testOrdExamId)
            => await _dbSet.AsNoTracking()
                .Where(q => q.TestId == testOrdExamId || q.ExamId == testOrdExamId)
                .CountAsync();
    }
}
