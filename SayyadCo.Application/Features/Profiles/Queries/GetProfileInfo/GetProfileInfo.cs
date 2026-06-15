using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Profiles.Queries.GetProfileInfo
{
    public class GetProfileInfoQuery : IRequest<Result<GetProfileInfoResponseDto>>
    {
    }
}
