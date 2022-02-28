using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UpdateProductCommand
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        public string Description { get; set; }
        public double Value { get; set; }
        public string Brand { get; set; }
        public long CategoryId { get; set; }
    }
}
