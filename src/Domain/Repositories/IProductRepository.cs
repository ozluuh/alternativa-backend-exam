using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<bool> NotExists(long id);
    }
}
