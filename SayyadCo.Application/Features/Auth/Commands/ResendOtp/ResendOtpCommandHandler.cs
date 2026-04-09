using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Common.Templates;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;
using System.Security.Cryptography;

namespace SayyadCo.Application.Features.Auth.Commands.ResendOtp
{
    public class ResendOtpCommandHandler : IRequestHandler<ResendOtpCommand, Result<ResendOtpResponseDto>>
    {
        private readonly IAuthService _authService;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public ResendOtpCommandHandler(IAuthService authService, IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork,
            IMapper mapper, IEmailService emailService)
        {
            _authService = authService;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Result<ResendOtpResponseDto>> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
        {
            var otp = await _unitOfWork.Otps.GetByTokenAsync(request.VerificationToken);
            if (otp is null)
                return Result<ResendOtpResponseDto>.NotFound("Invalid verification token");

            if (otp.IsLocked)
                return Result<ResendOtpResponseDto>.Failure(
                    $"Too many attempts. Try again after {otp.LockedUntil!.Value:HH:mm:ss} UTC");

            if (!otp.CanResend)
                return Result<ResendOtpResponseDto>.Failure("Please wait 1 minute before requesting a new code");

            await _unitOfWork.Otps.InvalidateUserOtpsAsync(otp.UserId);

            var user = await _authService.FindByIdAsync(otp.UserId);
            if (user is null)
                return Result<ResendOtpResponseDto>.NotFound("User not found");

            var newOtpCode = GenerateOtp();
            var newOtpEntity = new OtpCode
            {
                UserId = otp.UserId,
                Code = newOtpCode,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                LastResendAt = DateTime.UtcNow
            };

            await _unitOfWork.Otps.AddAsync(newOtpEntity);
            await _unitOfWork.SaveChangesAsync();

            await _emailService.SendAsync(user.Email, EmailTemplates.OtpTemplate(newOtpCode, user.FirstName));

            return Result<ResendOtpResponseDto>.Success(new ResendOtpResponseDto
            {
                Message = "A new verification code has been sent to your email.",
                VerificationToken = newOtpEntity.Token
            });
        }

        private static string GenerateOtp()
        {
            var number = RandomNumberGenerator.GetInt32(100000, 1000000);
            return number.ToString();
        }
    }
}
