using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_Alpha.Models.ViewModels
{
    public class ArticleVM
    {
        public Article Article { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
