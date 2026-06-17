using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<bool> ExistingByTitle(string title, string? id = null);
    }
}
