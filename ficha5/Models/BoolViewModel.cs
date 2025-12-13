using System.ComponentModel.DataAnnotations;

namespace ficha5.Models
{
    public class BoolViewModel
    {
        [Required(ErrorMessage = "The field is required.")]
        [StringLength(100, ErrorMessage = "The title cannot exceed 100 characters.")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "The second field is required.")]
        public IFormFile? Document { get; set; }

        [Required(ErrorMessage = "The third field is required.")]
        public IFormFile? CoverPhoto { get; set; }
    }
}
