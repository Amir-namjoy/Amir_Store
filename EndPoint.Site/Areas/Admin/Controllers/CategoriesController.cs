using Amir_Store.Application.Interfaces.FacadePatterns;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductFacade _productFacade;
        public CategoriesController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        [HttpGet]
        public IActionResult AddNewCategory(long? parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(long? parentId, string name)
        {
            var result = _productFacade.AddNewCategoryService.Execute(parentId, name);
            return Json(result);
        }
        public IActionResult Index(long? parentId)
        {
            return View(_productFacade.GetCategoriesService.Execute(parentId).Data);
        }
    }
}
