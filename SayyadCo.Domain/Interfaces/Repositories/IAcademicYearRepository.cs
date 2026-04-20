using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IAcademicYearRepository : IRepository<AcademicYear>
    {
        Task<bool> ExistsByTitles(string id, string titleAr, string titleEn);
    }
}
