using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Exams.Commands.RejectExam
{
    public class RejectExamCommand : IRequest<Result<bool>>
    {
        [JsonIgnore]
        public string ExamId { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }
}
