using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ficha2.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage ="requiered field")]
        [StringLength(50,MinimumLength =3, ErrorMessage ="{0} length must be tetwen {2} and {1}")]
        public string? name { get; set; }

        [Required(ErrorMessage ="length can not exed 1 characters")]
        [MaxLength(256,ErrorMessage = "{0} length can not exed {1} characters")]
        public string? description { get; set; }

        public Boolean State { get; set; } = true;

        [DisplayName("data de creacao")]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }=DateTime.Now;

    }
}
