using Amir_Store.Application.Interfaces.FacadePatterns;
using Amir_Store.Application.Services.Products.Queries.GetProductForSite;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductFacade _productFacade;
        public ProductsController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }
        [HttpGet]
        public IActionResult Index(string SearchKey, Ordering ordering = Ordering.MostVisited, long ? CatId = null, int page = 1, int pageSize=20)
        {
            return View(_productFacade.GetProductForSiteService.Execute(ordering, SearchKey, page, pageSize, CatId).Data);
        }

        public IActionResult Detail(long Id)
        {
            return View(_productFacade.GetProductDetailForSiteService.Execute(Id).Data);
        }
    }
}
