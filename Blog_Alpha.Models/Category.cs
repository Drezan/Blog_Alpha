using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
    }
}
