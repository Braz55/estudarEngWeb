using System.ComponentModel;
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

        [DisplayName("creation date")]
        public DateTime date { get; set; }= DateTime.Now;

        public Boolean State {  get; set; }= true;

        public ICollection<Course>? courses { get; set; }       //isto n é guardado na base de dados, serve apenas para poder fazer category.courses.name
    }
}
