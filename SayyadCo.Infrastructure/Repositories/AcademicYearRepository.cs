using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class AcademicYearRepository : Repository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByTitles(string id, string titleAr, string titleEn)
            => await _dbSet.AnyAsync(ay => ay.Id != id && (ay.TitleAr == titleAr || ay.TitleEn == titleEn));
    }
}
