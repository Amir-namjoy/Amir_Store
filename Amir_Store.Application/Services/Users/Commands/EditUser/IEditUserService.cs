using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Services.Users.Commands.EditUser
{
    public interface IEditUserService
    {
        ResultDto Execute(long id, string FullName);
    }

    public class EditUserServices : IEditUserService
    {
        private readonly IDataBaseContext _context;
        public EditUserServices(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long UserId, string FullName)
        {
            var user = _context.Users.Find(UserId);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد."
                };
            }
            user.FullName = FullName;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت ویرایش شد."
            };
        }
    }
}
