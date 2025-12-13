using System.ComponentModel.DataAnnotations;

namespace ficha5.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The field is required.")]
        [StringLength(100, ErrorMessage = "The title cannot exceed 100 characters.")]
        public string? Title { get; set; }
        [Required]
        public string? CoverPhoto { get; set; } //url da foto 
        [Required]
        public string? Document { get; set; } //url do documento
    }
}
