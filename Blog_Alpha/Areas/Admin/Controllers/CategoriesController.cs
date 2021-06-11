using Blog_Alpha.Data.Data.Repository;
using Blog_Alpha.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_Alpha.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public CategoriesController(IUnityOfWork UnityOfWork)
        {
            _UnityOfWork = UnityOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Created_At = DateTime.Now;
                _UnityOfWork.Category.Add(category);
                _UnityOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
            
        }

        [HttpGet]
        public IActionResult Edit(int ID)
        {
            Category Category = new Category();
            Category = _UnityOfWork.Category.Get(ID);

            if (Category == null)
                return NotFound();

            return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Created_At = DateTime.Now;
                _UnityOfWork.Category.Update(category);
                _UnityOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);

        }

        #region Calling API's
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _UnityOfWork.Category.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var Category = _UnityOfWork.Category.Get(Id);
            if (Category == null)
                return Json(new { success = false, message = "Something it's wrong trying to delete this category." });

            _UnityOfWork.Category.Delete(Category);
            _UnityOfWork.Save();

            return Json(new { success = true, message = "The Category deleted with success." });

        }
        #endregion
    }
}
