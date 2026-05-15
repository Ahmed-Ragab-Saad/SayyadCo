using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Exams.Commands.ApproveExam
{
    public class ApproveExamCommand : IRequest<Result<bool>>
    {
        public string ExamId { get; set; } = string.Empty;
    }
}
