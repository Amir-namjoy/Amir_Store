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

namespace Amir_Store.Application.Interfaces.FacadePatterns
{
    public interface IUserFacade
    {
        EditUserServices EditUserServices { get; }
        RegisterUserService RegisterUserService { get; }
        RemoveUserService RemoveUserService { get; }
        UserLoginService UserLoginService { get; }
        UserStatusChangeService UserStatusChangeService { get; }
        GetRolesService GetRolesService { get; }
        GetUsersService GetUsersService { get; }
    }
}
