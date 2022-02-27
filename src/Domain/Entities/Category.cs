using System.Collections.Generic;
using Domain.Commands.Inputs;

namespace Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public static implicit operator Category(StoreCategoryCommand entity) => new Category()
        {
            Name = entity.Name,
            Description = entity.Description
        };

        public static implicit operator Category(UpdateCategoryCommand entity) => new Category()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
        };
    }
}
