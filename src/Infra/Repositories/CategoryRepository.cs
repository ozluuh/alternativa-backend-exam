using System;
using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
