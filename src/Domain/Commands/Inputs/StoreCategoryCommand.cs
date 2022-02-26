using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Inputs
{
    public class StoreCategoryCommand
    {
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        public string Description { get; set; }
    }
}
