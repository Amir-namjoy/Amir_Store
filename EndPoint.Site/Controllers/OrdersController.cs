using Amir_Store.Application.Services.Orders.Queries.GetUserOrders;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IGetUserOrdersService _getUserOrders;

        public OrdersController(IGetUserOrdersService getUserOrders)
        {
            _getUserOrders = getUserOrders;
        }
        public IActionResult Index()
        {
            long userId = ClaimUtility.GetUserId(User).Value;
            return View(_getUserOrders.Execute(userId).Data);
        }
    }
}
