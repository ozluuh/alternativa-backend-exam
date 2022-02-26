using Domain.Commands.Inputs;

namespace Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static implicit operator Category(StoreCategoryCommand entity) => new Category()
        {
            Name = entity.Name,
            Description = entity.Description
        };
    }
}
