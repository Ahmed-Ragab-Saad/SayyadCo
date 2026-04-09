using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken token)
            => await _context.RefreshTokens.AddAsync(token);

        public async Task<RefreshToken?> GetByTokenAsync(string token)
            => await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == token);


        public async Task RevokeAsync(string token)
        {
            var refreshToken = await GetByTokenAsync(token);
            if (refreshToken is null)
                return;

            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;
        }

        public async Task RevokeAllUserTokensAsync(string userId)
        {
            var tokens = await _context.RefreshTokens
                .Where(t => t.UserId == userId && !t.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
                token.RevokedAt = DateTime.UtcNow;
            }
        }

    }
}
