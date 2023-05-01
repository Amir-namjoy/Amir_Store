using System.Collections.Generic;

namespace Amir_Store.Application.Services.Users.Queries.GetUsers
{
    public class ResultGetUserDto
    {
        public List<GetUsersDTO> Users { get; set; }
        public int Rows { get; set; }
    }

}
