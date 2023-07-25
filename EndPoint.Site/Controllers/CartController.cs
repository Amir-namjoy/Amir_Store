using Amir_Store.Application.Services.Carts;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartService;
        private readonly CookiesManager cookiesManeger;
        public CartController(ICartServices cartServices)
        {
            _cartService = cartServices;
            cookiesManeger = new CookiesManager();
        }
        public IActionResult Index()
        {
            var userId = ClaimUtility.GetUserId(User);
            var resultGetList = _cartService.GetMyCart(cookiesManeger.GetBrowserId(HttpContext), userId);
            return View(resultGetList.Data);
        }

        public IActionResult AddToCart(long productId)
        {
            var resultAdd = _cartService.AddToCart(productId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

        public IActionResult Add(long cartItemId)
        {
            _cartService.Add(cartItemId);
            return RedirectToAction("Index");
        }
        
        public IActionResult LowOff(long cartItemId)
        {
            _cartService.LowOff(cartItemId);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(long productId)
        {
            _cartService.RemoveFromCart(productId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }
    }
}
