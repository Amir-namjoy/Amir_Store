using Amir_Store.Application.Services.Finances.Queries.GetRequestPayForAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class PayController : Controller
    {
        private readonly IGetRequestPayForAdminService _getRequestPayForAdminService;
        public PayController(IGetRequestPayForAdminService getRequestPayForAdminService)
        {
            _getRequestPayForAdminService = getRequestPayForAdminService;
        }
        public IActionResult Index()
        {
            return View(_getRequestPayForAdminService.Execute().Data);
        }
    }
}
