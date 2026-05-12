using MediatR;
using SayyadCo.Application.Common.Results;
using System.Text.Json.Serialization;

namespace SayyadCo.Application.Features.Exams.Commands.RejectExamUpdate
{
    public class RejectExamUpdateCommand : IRequest<Result<bool>>
    {
        [JsonIgnore]
        public string RequestId { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}
