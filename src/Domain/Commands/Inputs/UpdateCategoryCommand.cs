using System.ComponentModel.DataAnnotations;

namespace Domain.Commands.Inputs
{
    public class UpdateCategoryCommand
    {
        // NOTE: Validation implemented for exam over api documentation
        [Required(ErrorMessage = "required")]
        public long Id { get; set; }

        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required")]
        public string Description { get; set; }
    }
}
