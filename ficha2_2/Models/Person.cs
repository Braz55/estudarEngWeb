using System.ComponentModel.DataAnnotations;

namespace ficha2_2.Models
{
    public class Person
    {
        [Required(ErrorMessage ="The field {0} is mandatory")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="the field is mandatory")]
        [Range(18,100,ErrorMessage ="must be between {18} {100}")]
        public int Age { get; set; }
    }
}
