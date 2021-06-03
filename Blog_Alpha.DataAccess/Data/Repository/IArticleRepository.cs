using Blog_Alpha.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog_Alpha.Data.Data.Repository
{
    public interface IArticleRepository : IRepository<Article>
    {
        void Update(Article article);
    }
}
