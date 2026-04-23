using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface ISectionGameAcademicYearRepository
    {
        Task AddAsync(SectionGameAcademicYear sectionGameAcademicYear);
        Task<SectionGameAcademicYear?> GetAsync(string sectionId, string gameId, string academicYearId);
    }
}
