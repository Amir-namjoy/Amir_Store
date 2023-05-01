using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common;
using System.Collections.Generic;
using System.Linq;

namespace Amir_Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _contex;
        public GetUsersService(IDataBaseContext contex)
        {
            _contex = contex;
        }

        public ResultGetUserDto Execute(RequestGetUsetDto request)
        {
            var users = _contex.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(p => p.FullName.Contains(request.SearchKey) || p.Email.Contains(request.SearchKey));
            }
            int rowsCount = 0;
            var usersList = users.ToPaged(request.Page, 20, out rowsCount).Select(p => new GetUsersDTO
            {
                Email = p.Email,
                FullName = p.FullName,
                Id = p.Id,
                IsActive = p.IsActive

            }).ToList();

            return new ResultGetUserDto
            {
                Rows = rowsCount,
                Users = usersList,
            };
        }
    }

}
