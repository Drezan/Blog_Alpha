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

        public IEnumerable<SelectListItem> GetAllArticles()
        {
            return _db.Articles.Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.Id.ToString()
            });
        }

        public void Update(Article article)
        {
            var oArticle = _db.Articles.FirstOrDefault(c => c.Id == article.Id);

            oArticle.Title = article.Title;
            oArticle.Description = article.Description;
            oArticle.MessageText = article.MessageText;
            oArticle.Record.Modified_At = DateTime.Now;

            _db.SaveChanges();
        }
    }
}
