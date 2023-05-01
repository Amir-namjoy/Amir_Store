using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Services.Users.Commands.UserStatusChange
{
    public interface IUserStatusChangeService
    {
        ResultDto Execute(long userId);
    }

    public class UserStatusChangeService : IUserStatusChangeService
    {
        private readonly IDataBaseContext _context;
        public UserStatusChangeService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return new ResultDto
                {
                    Message = "کاربر یافت نشد.",
                    IsSuccess = false,
                };
            }
            user.IsActive = !user.IsActive;
            _context.SaveChanges();
            string userState = user.IsActive == true ? "فعال" : "غیرفعال";
            return new ResultDto
            {
                IsSuccess = true,
                Message = $"کاربر با موفقیت {userState} شد."
            };
        }
    }
}
