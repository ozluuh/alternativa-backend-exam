using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class StoreProductCommand
    {
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        public string Description { get; set; }
        public double Value { get; set; }
        public string Brand { get; set; }
        public long CategoryId { get; set; }
    }
}
