using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Auth.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Result<VerifyEmailResponseDto>>
    {
        private readonly IAuthService _authService;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VerifyEmailCommandHandler(IAuthService authService, IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _authService = authService;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<VerifyEmailResponseDto>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var otp = await _unitOfWork.Otps.GetByTokenAsync(request.VerificationToken);
            if (otp is null)
                return Result<VerifyEmailResponseDto>.NotFound("Invalid verification token");

            if (otp.IsLocked)
                return Result<VerifyEmailResponseDto>.Failure(
                    $"Too many attempts. Try again after {otp.LockedUntil!.Value:HH:mm:ss} UTC");

            if (otp.IsExpired)
                return Result<VerifyEmailResponseDto>.Failure("OTP has expired");

            if (otp.Code != request.OtpCode)
            {
                otp.FailedAttempts++;

                if (otp.FailedAttempts >= 5)
                    otp.LockedUntil = DateTime.UtcNow.AddMinutes(5);

                await _unitOfWork.SaveChangesAsync();

                var remaining = 5 - otp.FailedAttempts;
                return remaining > 0
                    ? Result<VerifyEmailResponseDto>.Failure($"Invalid OTP. {remaining} attempts remaining")
                    : Result<VerifyEmailResponseDto>.Failure("Too many failed attempts. Try again in 5 minutes");
            }

            await _unitOfWork.Otps.InvalidateUserOtpsAsync(otp.UserId);
            var confirmed = await _authService.ConfirmEmailAsync(otp.UserId);
            if (!confirmed)
                return Result<VerifyEmailResponseDto>.Failure("Failed to confirm email");

            var user = await _authService.FindByIdAsync(otp.UserId);
            var roles = await _authService.GetRolesAsync(otp.UserId);

            var userModel = new UserTokenModel
            {
                Id = user!.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles
            };

            var tokens = _jwtGenerator.GenerateTokens(userModel);

            await _unitOfWork.RefreshTokens.AddAsync(new RefreshToken
            {
                Token = tokens.RefreshToken,
                UserId = user.Id,
                ExpiresAt = tokens.RefreshTokenExpiry
            });

            await _unitOfWork.SaveChangesAsync();

            return Result<VerifyEmailResponseDto>.Success(_mapper.Map<VerifyEmailResponseDto>(tokens));
        }
    }
}