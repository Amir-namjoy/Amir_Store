using Amir_Store.Application.Interfaces.FacadePatterns;
using Amir_Store.Application.Services.Common.Queries.GetHomePageImages;
using Amir_Store.Application.Services.Common.Queries.GetSliders;
using Amir_Store.Application.Services.Products.Queries.GetProductForSite;
using EndPoint.Site.Models;
using EndPoint.Site.Models.HomePage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetSliderService _getSliderService;
        private readonly IGetHomePageImagesService _getHomePageImagesService;
        private readonly IProductFacade _productFacade;

        public HomeController(ILogger<HomeController> logger, IGetSliderService getSliderService, 
            IGetHomePageImagesService getHomePageImagesService, IProductFacade productFacade)
        {
            _logger = logger;
            _getSliderService = getSliderService;
            _getHomePageImagesService = getHomePageImagesService;
            _productFacade = productFacade;
        }

        public IActionResult Index()
        {
            HomePageViewModel homePageviewModel = new HomePageViewModel()
            {
                Sliders = _getSliderService.Execute().Data,
                PageImages = _getHomePageImagesService.Execute().Data,
                Camera = _productFacade.GetProductForSiteService.Execute(Ordering.theNewest, null, 1, 6, 10002).Data.Products,
                Mobile = _productFacade.GetProductForSiteService.Execute(Ordering.theNewest, null, 1, 6, 10003).Data.Products,
                HomeAppliances = _productFacade.GetProductForSiteService.Execute(Ordering.theNewest, null, 1, 6, 10004).Data.Products,
                Laptop = _productFacade.GetProductForSiteService.Execute(Ordering.theNewest, null, 1, 6, 1).Data.Products,
            };
            return View(homePageviewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
