using System;
using System.ComponentModel.DataAnnotations;

namespace Blog_Alpha.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Category's Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Category's Order")]
        public string Order { get; set; }

        //Records. Nota: Buscar manera de simplificar esto.
        public Nullable<int> Created_By { get; set; }
        public Nullable<int> Modified_By { get; set; }
        public Nullable<DateTime> Created_At { get; set; } = DateTime.Now;
        public Nullable<DateTime> Modified_At { get; set; }
    }
}
