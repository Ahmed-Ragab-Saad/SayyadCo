using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Sections.Commands.DeleteSection
{
    public class DeleteSectionCommand : IRequest<Result<bool>>
    {
        public string Id { get; set; } = string.Empty;
    }
}
