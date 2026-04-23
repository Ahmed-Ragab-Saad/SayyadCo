using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class SectionGameAcademicYearRepository : ISectionGameAcademicYearRepository
    {
        private readonly AppDbContext _context;

        public SectionGameAcademicYearRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SectionGameAcademicYear sectionGameAcademicYear)
            => await _context.SectionGameAcademicYears.AddAsync(sectionGameAcademicYear);

        public async Task<SectionGameAcademicYear?> GetAsync(string sectionId, string gameId, string academicYearId)
            => await _context.SectionGameAcademicYears
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.SectionId == sectionId &&
                    x.GameId == gameId &&
                    x.AcademicYearId == academicYearId);
    }
}
