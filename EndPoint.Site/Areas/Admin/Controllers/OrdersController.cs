using Amir_Store.Application.Services.Orders.Queries.GetOrdersForAdmin;
using Amir_Store.Domain.Entities.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,Operator")]
    public class OrdersController : Controller
    {
        private readonly IGetOrdersForAdmin _getOrdersForAdmin;

        public OrdersController(IGetOrdersForAdmin getOrdersForAdmin)
        {
            _getOrdersForAdmin = getOrdersForAdmin;
        }
        public IActionResult Index(OrderState orderState)
        {
            return View(_getOrdersForAdmin.Execute(orderState).Data);
        }
    }
}
