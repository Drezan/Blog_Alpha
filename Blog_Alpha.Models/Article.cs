using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog_Alpha.Models
{
    public class Article : Records
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, write a title for this article.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please, write a message for this article.")]
        public string MessageText { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")] //creo la llave foránea y lo referencio a la propiedad CategoryId
        public Category Category { get; set; } //Aquí le digo que se relaciona a la tabla Category

        public Article() { }
        
        public Article(int? Created_By, int? Modified_By, DateTime? Created_At, DateTime? Modified_At)
        {
            this.Created_By = Created_By;
            this.Modified_By = Modified_By;
            this.Created_At = Created_At;
            this.Modified_At = Modified_At;
        }

    }
}
