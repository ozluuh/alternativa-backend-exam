using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<bool> HasDependent(long id);
    }
}
