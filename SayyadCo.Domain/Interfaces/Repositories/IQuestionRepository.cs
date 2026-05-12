using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task AddQuestions(IList<Question> questionList);
        Task<int> GetQuestionsCount(string testOrdExamId);
    }
}
