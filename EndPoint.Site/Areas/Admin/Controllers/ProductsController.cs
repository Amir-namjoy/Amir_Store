using Amir_Store.Application.Interfaces.FacadePatterns;
using Amir_Store.Application.Services.Products.Commands.AddNewProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class ProductsController : Controller
    {
        private readonly IProductFacade  _productFacade;
        public ProductsController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public IActionResult Index(int Page = 1, int PageSize = 20)
        {
            return View(_productFacade.GetProductForAdminServices.Execute(Page, PageSize).Data);
        }

        public IActionResult Detail(long Id)
        {
            return View(_productFacade.GetProductDetailForAdminService.Execute(Id).Data);
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFacade.GetAllCategoriesService.Execute().Data,"Id","Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(RequestAddNewProductDto request, List<AddNewProduct_Features> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            //foreach (var file in request) { }
            for (int i = 0; i < Request.Form.Files.Count; i++) 
            {
                images.Add(Request.Form.Files[i]);
            }
            request.Images = images;
            request.Features = Features;
            return Json(_productFacade.AddNewProductService.Execute(request));
        }
    }
}
