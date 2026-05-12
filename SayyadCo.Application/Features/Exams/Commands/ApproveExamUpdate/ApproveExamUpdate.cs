using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Exams.Commands.ApproveExamUpdate
{
    public class ApproveExamUpdateCommand : IRequest<Result<bool>>
    {
        [JsonIgnore]
        public string RequestId { get; set; } = string.Empty;
    }
}
