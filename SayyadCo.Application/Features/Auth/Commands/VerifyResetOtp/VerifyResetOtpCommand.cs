using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Auth.Commands.VerifyResetOtp
{
    public class VerifyResetOtpCommand : IRequest<Result<VerifyResetOtpResponseDto>>
    {
        public string ResetToken { get; set; } = string.Empty;
        public string OtpCode { get; set; } = string.Empty;
    }
}
