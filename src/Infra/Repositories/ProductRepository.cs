using System;
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

        public override async Task<Product> CreateAsync(Product entity)
        {
            await base.CreateAsync(entity);

            _context.Entry(entity).Reference(p => p.Category).Load();

            return entity;
        }
    }
}
