using Amir_Store.Application.Services.HomePage.AddHomePageImages;
using Amir_Store.Domain.Entities.HomePage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageImagesController : Controller
    {
        private readonly IAddHomePageImagesService _addHomePageImagesService;
        public HomePageImagesController(IAddHomePageImagesService addHomePageImagesService)
        {
            _addHomePageImagesService = addHomePageImagesService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file, string link, ImageLocation imageLocation) 
        {
            _addHomePageImagesService.Execute(new requestAddHomePageImage
            { 
                file = file,
                link = link,
                imageLocation = imageLocation
            });
            return View();
        }
    }
}
