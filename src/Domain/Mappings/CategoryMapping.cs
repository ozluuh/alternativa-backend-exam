using Domain.Commands.Inputs;
using Domain.Commands.Results;
using Domain.Entities;

namespace Domain.Mappings
{
    public static class CategoryMapping
    {
        public static CategoryCommandResult ToCommandResult(this Category entity)
            => new CategoryCommandResult()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };

        public static Category ToCategory(this StoreCategoryCommand entity)
            => new Category()
            {
                Name = entity.Name,
                Description = entity.Description
            };

        public static Category ToCategory(this UpdateCategoryCommand entity)
            => new Category()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
    }
}
