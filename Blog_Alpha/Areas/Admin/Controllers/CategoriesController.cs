using Blog_Alpha.Data.Data.Repository;
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
        public IActionResult Index()
        {
            return View();
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
