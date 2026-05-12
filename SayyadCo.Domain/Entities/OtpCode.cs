using SayyadCo.Domain.Common;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Entities
{
    public class OtpCode : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public OtpType Type { get; set; } = OtpType.EmailVerification;
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; } = false;
        public int FailedAttempts { get; set; } = 0;
        public DateTime? LockedUntil { get; set; }
        public DateTime? LastResendAt { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsLocked => LockedUntil.HasValue && DateTime.UtcNow < LockedUntil.Value;
        public bool IsValid => !IsUsed && !IsExpired && !IsLocked;
        public bool CanResend => !LastResendAt.HasValue || DateTime.UtcNow >= LastResendAt.Value.AddMinutes(1);
    }
}
