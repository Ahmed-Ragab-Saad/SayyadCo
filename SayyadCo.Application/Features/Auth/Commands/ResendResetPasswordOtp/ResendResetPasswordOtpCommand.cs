using MediatR;
using SayyadCo.Application.Common.Results;

namespace SayyadCo.Application.Features.Auth.Commands.ResendResetPasswordOtp
{
    public class ResendResetPasswordOtpCommand : IRequest<Result<ResendResetPasswordOtpResponseDto>>
    {
        public string ResetToken { get; set; } = string.Empty;
    }
}
