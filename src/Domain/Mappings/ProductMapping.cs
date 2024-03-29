using Domain.Commands.Results;
using Domain.Entities;

namespace Domain.Mappings
{
    public static class ProductMapping
    {
        public static ProductCommandResult ToCommandResult(this Product entity)
            => new ProductCommandResult()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Value = entity.Value.GetValueOrDefault(0),
                Brand = entity.Brand,
                CategoryId = entity.CategoryId,
                Category = entity.Category?.ToCommandResult()
            };

        public static Product ToDomain(this StoreProductCommand entity)
            => new Product()
            {
                Name = entity.Name,
                Description = entity.Description,
                Value = entity.Value,
                Brand = entity.Brand,
                CategoryId = entity.CategoryId
            };

        public static Product ToDomain(this UpdateProductCommand entity)
            => new Product()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Value = entity.Value,
                Brand = entity.Brand,
                CategoryId = entity.CategoryId
            };
    }
}
