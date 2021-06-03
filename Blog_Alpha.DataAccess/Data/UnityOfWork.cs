using Blog_Alpha.Data.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_Alpha.Data.Data
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnityOfWork(ApplicationDbContext db)
        {
            _db = db;

            Category = new CategoryRepository(_db);
            Article = new ArticleRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public IArticleRepository Article { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
