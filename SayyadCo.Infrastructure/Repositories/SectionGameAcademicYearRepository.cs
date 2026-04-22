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
    }
}
