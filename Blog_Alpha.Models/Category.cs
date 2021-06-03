using System;
using System.ComponentModel.DataAnnotations;

namespace Blog_Alpha.Models
{
    public class Category : Records
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Category's Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Category's Order")]
        public int Order { get; set; }
        public Category() { }

        public Category(int? Created_By, int? Modified_By, DateTime? Created_At, DateTime? Modified_At)
        {
            this.Name = "";
            this.Order = 0;
            this.Created_By = Created_By;
            this.Modified_By = Modified_By;
            this.Created_At = Created_At;
            this.Modified_At = Modified_At;
        }

        //Records. Nota: Buscar manera de simplificar esto.
        //public Nullable<int> Created_By { get; set; }
        //public Nullable<int> Modified_By { get; set; }
        //public Nullable<DateTime> Created_At { get; set; } = DateTime.Now;
        //public Nullable<DateTime> Modified_At { get; set; }
    }
}
