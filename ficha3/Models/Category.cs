using System.ComponentModel.DataAnnotations;

namespace ficha3.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage ="requiered field")]
        [StringLength(50,MinimumLength = 3,ErrorMessage ="o tamanho da string tem que ser entre 2 e 1")]
        public string? name { get; set; }

        [Required(ErrorMessage = "requiered field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "o tamanho da string tem que ser entre 2 e 1")]
        public string? description { get; set; }

        public Boolean State {  get; set; }
    }
}
