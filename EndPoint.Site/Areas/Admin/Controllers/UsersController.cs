using Amir_Store.Application.Services.Users.Commands.RegisterUser;
using Amir_Store.Application.Services.Users.Queries.GetRoles;
using Amir_Store.Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Amir_Store.Application.Services.Users.Commands.RemoveUser;
using Amir_Store.Application.Services.Users.Commands.UserStatusChange;
using Amir_Store.Application.Services.Users.Commands.EditUser;
using Amir_Store.Application.Interfaces.FacadePatterns;
using Amir_Store.Application.Services.Users.FacadePatterns;
using Microsoft.AspNetCore.Authorization;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        //private readonly IGetUsersService _getUsersService;
        //private readonly IGetRolesService _getRolesService;
        //private readonly IRegisterUserService _RegisterUserService;
        //private readonly IRemoveUserService _removeUserService;
        //public readonly IUserStatusChangeService _userStatusChangeService;
        //private readonly IEditUserService _EditUserService;
        //public UsersController(IGetUsersService getUsersService
        //    , IGetRolesService getRolesService
        //    , IRegisterUserService RegisterUserService
        //    , IRemoveUserService removeUserService
        //    , IUserStatusChangeService userStatusChangeService
        //    , IEditUserService EditUserService)
        //{
        //    _getUsersService = getUsersService;
        //    _getRolesService = getRolesService;
        //    _RegisterUserService = RegisterUserService;
        //    _removeUserService = removeUserService;
        //    _userStatusChangeService = userStatusChangeService;
        //    _EditUserService = EditUserService;
        //}
        private readonly IUserFacade _userFacade;
        public UsersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public IActionResult Index(string SerchKey, int page = 1)
        {
            return View(_userFacade.GetUsersService.Execute(new RequestGetUsetDto
            {
                Page = page,
                SearchKey = SerchKey,
            }));
        }

        [HttpGet]
        public IActionResult create()
        {
            ViewBag.Roles = new SelectList(_userFacade.GetRolesService.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Email, string FullName, long RoleId, string Password, string RePassword)
        {
            var result = _userFacade.RegisterUserService.Execute(new RequestRegisterUserDto
            {
                Email = Email,
                FullName = FullName,
                roles = new List<RolesInRegisterUserDto>()
                {
                    new RolesInRegisterUserDto()
                    {
                        Id = RoleId
                    }
                },
                Password = Password,
                RePassword = RePassword,
            });
            return Json(result);
        }
        
        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_userFacade.RemoveUserService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult UserStatusChange(long UserId)
        {
            return Json(_userFacade.UserStatusChangeService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult EditUser(long UserId, string FullName)
        {
            return Json(_userFacade.EditUserServices.Execute(UserId, FullName));
        }
    }
}