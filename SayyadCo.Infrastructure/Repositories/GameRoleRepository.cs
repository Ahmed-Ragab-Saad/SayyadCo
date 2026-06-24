using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class GameRoleRepository : IGameRoleRepository
    {
        private readonly AppDbContext _context;

        public GameRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(GameRole gameRole)
            => await _context.GameRoles.AddAsync(gameRole);

        public async Task<GameRole?> GetByIdAsync(string id)
            => await _context.GameRoles.AsNoTracking().FirstOrDefaultAsync(gr => gr.Id == id);

        public async Task<string?> GetRoleNameAsync(string id)
            => await _context.GameRoles
                .Where(gr => gr.Id == id)
                .Select(gr => gr.Role)
                .FirstOrDefaultAsync();

        public async Task<bool> IsRoleNameExist(string roleName)
            => await _context.GameRoles.AnyAsync(gr => gr.Role.ToUpper() == roleName.ToUpper());
    }
}
