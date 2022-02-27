using Domain.Entities;

namespace Domain.Commands.Results
{
    public class CategoryCommandResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static implicit operator CategoryCommandResult(Category entity)
            => new CategoryCommandResult()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
    }
}
