using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class OtpRepository : IOtpRepository
    {
        private readonly AppDbContext _context;

        public OtpRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OtpCode otp)
            => await _context.OtpCodes.AddAsync(otp);

        public async Task<OtpCode?> GetActiveOtpAsync(string userId)
            => await _context.OtpCodes
                .Where(o => o.UserId == userId && !o.IsUsed && o.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(o => o.ExpiresAt)
                .FirstOrDefaultAsync();

        public async Task InvalidateUserOtpsAsync(string userId)
        {
            var otps = await _context.OtpCodes
                .Where(o => o.UserId == userId && !o.IsUsed)
                .ToListAsync();

            foreach (var otp in otps)
                otp.IsUsed = true;
        }

        public async Task<OtpCode?> GetByTokenAsync(string token)
            => await _context.OtpCodes
                .FirstOrDefaultAsync(o => o.Token == token && !o.IsUsed);
    }
}
