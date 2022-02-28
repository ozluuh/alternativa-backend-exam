using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<bool> NotExistsOrHasDependents(long id)
        {
            Category category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return true;
            }

            int relatedDependents = _context
                                        .Entry(category)
                                        .Collection(p => p.Products)
                                        .Query()
                                        .Count();

            return relatedDependents > 0;
        }
    }
}
