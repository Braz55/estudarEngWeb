using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ficha3.Models
{
    public class Course
    {
        public int id { get; set; }

        [Required(ErrorMessage ="mandatory field")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="n tem o tamanho certo")]
        public string? name { get; set; }

        [Required(ErrorMessage = "mandatory field")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "n tem o tamanho certo")]
        public string? description { get; set; }

        public int credits { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName ="money")]
        public decimal cost { get; set; }

        public int categoryId { get; set; }

        public Category? category { get; set; }     //isto n é guardado na base de dados é para poder aceder ao objeto da ccategoria diretamente, fazendo algo como course.category.name
    }
}
