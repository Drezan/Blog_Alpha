using Blog_Alpha.Data.Data.Repository;
using Blog_Alpha.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog_Alpha.Data.Data
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _db;
        public ArticleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
        }

        public void Update(Article article)
        {
            var oArticle = _db.Articles.FirstOrDefault(c => c.Id == article.Id);

            oArticle.Title = article.Title;
            oArticle.Description = article.Description;
            oArticle.MessageText = article.MessageText;
            oArticle.ImageUrl = article.ImageUrl;
            oArticle.Modified_At = DateTime.Now;
            oArticle.CategoryId = article.CategoryId;
        }
    }
}
