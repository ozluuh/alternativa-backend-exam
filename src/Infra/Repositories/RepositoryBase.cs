using System;
using System.Collections.Generic;
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

        public virtual async Task<E> CreateAsync(E entity)
        {
            await _context.Set<E>().AddAsync(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(long id)
        {
            E entity = await GetByIdAsync(id);
            await Task.Run(() => _context.Set<E>().Remove(entity));
        }

        public virtual async Task<IEnumerable<E>> GetAllAsync()
        {
            return await _context.Set<E>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<E> GetByIdAsync(long id)
        {
            return await _context
                            .Set<E>()
                            .AsNoTracking()
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public virtual async Task<E> UpdateAsync(E entity)
        {
            await Task.Run(() => _context.Set<E>().Update(entity));
            return entity;
        }
    }
}
