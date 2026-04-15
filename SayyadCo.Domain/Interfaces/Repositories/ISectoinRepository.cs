using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface ISectoinRepository : IRepository<Section>
    {
        Task<Section?> GetFunnySection();
    }
}
