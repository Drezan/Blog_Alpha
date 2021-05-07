using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog_Alpha.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please, write a title for this article.")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please, write a message for this article.")]
        public string MessageText { get; set; }
        public Records Record { get; set; }
        

        public Article()
        {
            this.Title = "";
            this.Description = "";
            this.MessageText = "";
            this.Record = new Records();
        }

    }
}
