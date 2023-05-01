using Amir_Store.Application.Interfaces.FacadePatterns;
using Amir_Store.Application.Services.Products.Commands.AddNewProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductFacade  _productFacade;
        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFacade.GetAllCategoriesService.Execute().Data,"Id","Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(RequestAddNewProductDto request, List<AddNewProduct_Features> features)
        {
            List<IFormFile> images = new List<IFormFile>();
            foreach (var file in request) { }
            for (int i = 0; i < features.Count; i++)
            {

            }

        }
    }
}
