using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Application.Interfaces.FacadePatterns;
using Amir_Store.Application.Services.Users.Commands.EditUser;
using Amir_Store.Application.Services.Users.Commands.RegisterUser;
using Amir_Store.Application.Services.Users.Commands.RemoveUser;
using Amir_Store.Application.Services.Users.Commands.UserLogin;
using Amir_Store.Application.Services.Users.Commands.UserStatusChange;
using Amir_Store.Application.Services.Users.Queries.GetRoles;
using Amir_Store.Application.Services.Users.Queries.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Services.Users.FacadePatterns
{
    public class UserFacade : IUserFacade
    {
        private readonly IDataBaseContext _context;
        public UserFacade(IDataBaseContext context)
        {
            _context = context;
        }
        private EditUserServices _editUserServices;
        private RegisterUserService _registerUserService;
        private RemoveUserService _removeUserService;
        private UserLoginService _userLoginService;
        private UserStatusChangeService _userStatusChangeService;
        private GetRolesService _getRolesService;
        private GetUsersService _getUsersService;

        public EditUserServices EditUserServices 
        {
            get 
            {
                return _editUserServices ??= new EditUserServices(_context);
            } 
        }

        public RegisterUserService RegisterUserService
        {
            get
            {
                return _registerUserService ??= new RegisterUserService(_context);
            }
        }

        public RemoveUserService RemoveUserService
        {
            get
            {
                return _removeUserService ??= new RemoveUserService(_context);
            }
        }

        public UserLoginService UserLoginService
        {
            get
            {
                return _userLoginService ??= new UserLoginService(_context);
            }
        }

        public UserStatusChangeService UserStatusChangeService
        {
            get
            {
                return _userStatusChangeService ??= new UserStatusChangeService(_context);
            }
        }

        public GetRolesService GetRolesService
        {
            get
            {
                return _getRolesService ??= new GetRolesService(_context);
            }
        }

        public GetUsersService GetUsersService
        {
            get
            {
                return _getUsersService ??= new GetUsersService(_context);
            }
        }
    }
}
