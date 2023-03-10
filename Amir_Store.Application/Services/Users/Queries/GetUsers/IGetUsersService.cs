using Amir_Store.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amir_Store.Common;

namespace Amir_Store.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        List<GetUsersDTO> Execute(string SearchKey, int page=1);
    }
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _contex;
        public GetUsersService(IDataBaseContext contex)
        {
            _contex = contex;
        }

        public List<GetUsersDTO> Execute(string SearchKey, int page = 1)
        {
            var users = _contex.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(SearchKey))
            {
                users = users.Where(p => p.FullName.Contains(SearchKey) && p.Email.Contains(SearchKey));
            }
            int rowsCount = 0;
            return users.ToPaged(page, 20, out rowsCount).Select(p => new GetUsersDTO
            {
                Email = p.Email,
                FullName = p.FullName,
                Id = p.Id,

            }).ToList();
        }
    }
    
    public class ResultGetUserDTO
    {

    }
    public class GetUsersDTO
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

}
