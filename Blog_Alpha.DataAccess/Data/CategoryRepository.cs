using Blog_Alpha.Data.Data.Repository;
using Blog_Alpha.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog_Alpha.Data.Data
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
        }

        public IEnumerable<SelectListItem> GetAllCategories()
        {
            return _db.Categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var oCategory = _db.Categories.FirstOrDefault(c => c.Id == category.Id);

            oCategory.Name = category.Name;
            oCategory.Order = category.Order;
            oCategory.Modified_At = DateTime.Now;

            _db.SaveChanges();
        }
    }
}
