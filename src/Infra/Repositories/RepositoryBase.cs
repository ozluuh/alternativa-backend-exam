using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Repositories
{
    public class RepositoryBase<E> : IRepositoryBase<E> where E : EntityBase
    {
        protected readonly ApiDbContext _context;

        public RepositoryBase(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<ApiDbContext>();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(E entity)
        {
            await _context.Set<E>().AddAsync(entity);
        }

        public async Task DeleteAsync(long id)
        {
            E entity = await GetByIdAsync(id);
            await Task.Run(() => _context.Set<E>().Remove(entity));
        }

        public async Task<IQueryable<E>> GetAllAsync()
        {
            return await Task.Run(() => _context.Set<E>().AsNoTracking());
        }

        public async Task<E> GetByIdAsync(long id)
        {
            return await _context
                            .Set<E>()
                            .AsNoTracking()
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(E entity)
        {
            await Task.Run(() => _context.Set<E>().Update(entity));
        }
    }
}
