using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class SectionRepository : Repository<Section>, ISectoinRepository
    {
        public SectionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
