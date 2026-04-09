using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Auth.Commands.ResendOtp
{
    public class ResendOtpCommand : IRequest<Result<ResendOtpResponseDto>>
    {
        public string VerificationToken { get; set; } = string.Empty;
    }
}
