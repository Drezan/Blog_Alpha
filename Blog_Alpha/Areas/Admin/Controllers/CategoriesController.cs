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

        #region Calling API's
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _UnityOfWork.Category.GetAll() });
        }
        #endregion
    }
}
