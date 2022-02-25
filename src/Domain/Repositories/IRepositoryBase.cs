using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRepositoryBase<E> where E : EntityBase
    {
        Task<IEnumerable<E>> GetAllAsync();
        Task<E> GetByIdAsync(long id);
        Task CreateAsync(E entity);
        Task UpdateAsync(E entity);
        Task DeleteAsync(long id);
        Task<int> CommitAsync();
    }
}
