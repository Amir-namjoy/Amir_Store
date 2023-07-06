using Amir_Store.Application.Services.Carts;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class Cart:ViewComponent
    {
        private readonly ICartServices _cartServices;
        private readonly CookiesManeger _cookiesManeger;
        public Cart(ICartServices cartServices)
        {
            _cartServices = cartServices;
            _cookiesManeger = new CookiesManeger();
        }

        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            return View(viewName: "Cart", _cartServices.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), userId).Data);
        }
    }
}
