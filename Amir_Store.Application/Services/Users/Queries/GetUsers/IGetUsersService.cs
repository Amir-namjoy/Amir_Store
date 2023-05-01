using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        ResultGetUserDto Execute(RequestGetUsetDto request);
    }

}
