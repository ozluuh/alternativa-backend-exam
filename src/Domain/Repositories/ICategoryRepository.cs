using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<bool> NotExistsOrHasDependents(long id);
    }
}
