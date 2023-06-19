using Amir_Store.Application.Services.Common.Queries.GetMenuItem;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class GetMenu : ViewComponent
    {
        private readonly IGetMenuItemService _getMenuItemService;
        public GetMenu(IGetMenuItemService getMenuItemService)
        {
            _getMenuItemService = getMenuItemService;
        }

        public IViewComponentResult Invoke(bool mobile = false)
        {
            var menuItem = _getMenuItemService.Execute();
            if (mobile)
                return View(viewName: "GetMenuMobile", menuItem.Data);
            else
                return View(viewName: "GetMenu", menuItem.Data);
        }
    }
}
