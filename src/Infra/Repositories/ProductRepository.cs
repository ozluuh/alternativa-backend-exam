using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public override async Task<Product> GetByIdAsync(long id)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        private Product IncludeReferences(Product entity)
        {
            _context.Entry(entity).Reference(p => p.Category).Load();

            return entity;
        }

        public override async Task<Product> CreateAsync(Product entity)
        {
            await base.CreateAsync(entity);
            return IncludeReferences(entity);
        }

        public override async Task<Product> UpdateAsync(Product entity)
        {
            await base.UpdateAsync(entity);
            return IncludeReferences(entity);
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
