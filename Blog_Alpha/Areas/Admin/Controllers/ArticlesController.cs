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

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            ArticleVM ArticleVM = new ArticleVM()
            {
                Article = new Models.Article(),
                CategoryList = _UnityOfWork.Category.GetAllCategories()
            };

            if (Id != null)
            {
                ArticleVM.Article = _UnityOfWork.Article.Get(Id.GetValueOrDefault());
            }

            return View(ArticleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                string UrlImage = _WebHostEnviroment.WebRootPath;
                var Files = HttpContext.Request.Form.Files;

                var oArticle = _UnityOfWork.Article.Get(articleVM.Article.Id);

                if (Files.Count() > 0)
                {
                    string FileName = Guid.NewGuid().ToString();
                    string UploadFile = Path.Combine(UrlImage, @"Images\Articles");
                    var GetExtension = Path.GetExtension(Files[0].FileName);
                    var NewExtension = Path.GetExtension(Files[0].FileName);

                    var RouteImage = Path.Combine(UrlImage, oArticle.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(RouteImage))
                    {
                        System.IO.File.Delete(RouteImage);
                    }

                    //Se sube nuevamente el archivo
                    using (var fileStream = new FileStream(Path.Combine(UploadFile, FileName + NewExtension), FileMode.Create))
                    {
                        Files[0].CopyTo(fileStream);
                    }

                    articleVM.Article.ImageUrl = @"Images\Articles\" + FileName + NewExtension;
                    articleVM.Article.Modified_At = DateTime.Now;

                    _UnityOfWork.Article.Update(articleVM.Article);
                    _UnityOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    articleVM.Article.ImageUrl = oArticle.ImageUrl;
                }

                _UnityOfWork.Article.Update(articleVM.Article);
                _UnityOfWork.Save();
            }

            articleVM.CategoryList = _UnityOfWork.Category.GetAllCategories();

            return View(articleVM);
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var oArticle = _UnityOfWork.Article.Get(Id);
            string UrlImage = _WebHostEnviroment.WebRootPath;

            var ImageRoute = Path.Combine(UrlImage, oArticle.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(ImageRoute))
            {
                System.IO.File.Delete(ImageRoute);
            }

            if (oArticle == null)
            {
                return Json(new { success = false, message = "We have a problem trying to delete this article!" });
            }

            _UnityOfWork.Article.Delete(oArticle);
            _UnityOfWork.Save();

            return Json(new { success = true, message = "Article deleted successfully" });

        }

        #region Calling API's
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _UnityOfWork.Article.GetAll(IncludeProperties: "Category") });
        }
        #endregion
    }
}
