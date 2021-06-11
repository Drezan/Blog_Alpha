using Blog_Alpha.Data.Data.Repository;
using Blog_Alpha.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_Alpha.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;
        private readonly IWebHostEnvironment _WebHostEnviroment;
        public ArticlesController(IUnityOfWork UnityOfWork, IWebHostEnvironment WebHostEnviroment)
        {
            _UnityOfWork = UnityOfWork;
            _WebHostEnviroment = WebHostEnviroment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticleVM ArticleVM = new ArticleVM()
            {
                Article = new Models.Article(),
                CategoryList = _UnityOfWork.Category.GetAllCategories()
            };

            return View(ArticleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                string UrlImage = _WebHostEnviroment.WebRootPath;
                var Files = HttpContext.Request.Form.Files;

                if (articleVM.Article.Id == 0)
                {
                    string FileName = Guid.NewGuid().ToString();
                    string UploadFile = Path.Combine(UrlImage, @"Images\Articles");
                    var GetExtension = Path.GetExtension(Files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(UploadFile, FileName + GetExtension), FileMode.Create))
                    {
                        Files[0].CopyTo(fileStream);
                    }

                    articleVM.Article.ImageUrl = @"Images\Articles\" + FileName + GetExtension;
                    articleVM.Article.Created_At = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                    _UnityOfWork.Article.Add(articleVM.Article);
                    _UnityOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
            }

            articleVM.CategoryList = _UnityOfWork.Category.GetAllCategories();

            return View(articleVM);
        }

        #region Calling API's
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _UnityOfWork.Article.GetAll(IncludeProperties: "Category") });
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var Article = _UnityOfWork.Article.Get(Id);
            if (Article == null)
                return Json(new { success = false, message = "Something it's wrong trying to delete this article." });

            _UnityOfWork.Article.Delete(Article);
            _UnityOfWork.Save();

            return Json(new { success = true, message = "The Article deleted with success." });

        }
        #endregion
    }
}
