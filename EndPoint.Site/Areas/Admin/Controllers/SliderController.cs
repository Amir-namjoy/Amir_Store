using Amir_Store.Application.Services.HomePage.AddNewSlider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class SliderController : Controller
    {
        private readonly IAddNewSliderService _addNewSliderService;
        public SliderController(IAddNewSliderService addNewSliderService)
        {
            _addNewSliderService = addNewSliderService;
        }
        public IActionResult Index()
        {
            // Complete code for View list of sliders in admin panel
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file, string link)
        {
            _addNewSliderService.Execute(file, link);
            return View();
        }
    }
}
