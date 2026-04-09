using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Auth.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<Result<VerifyEmailResponseDto>>
    {
        public string VerificationToken { get; set; } = string.Empty;
        public string OtpCode { get; set; } = string.Empty;
    }
}
