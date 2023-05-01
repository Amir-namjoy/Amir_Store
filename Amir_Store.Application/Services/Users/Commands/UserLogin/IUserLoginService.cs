using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using Bugeto_Store.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Services.Users.Commands.UserLogin
{
    public interface IUserLoginService
    {
        ResultDto<ResultUserLoginDto> Execute(string UserName, string Password);
    }

    public class UserLoginService : IUserLoginService
    {
        private readonly IDataBaseContext _context;
        public UserLoginService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultUserLoginDto> Execute(string UserName, string Password)
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return new ResultDto<ResultUserLoginDto>()
                {
                    Data = new ResultUserLoginDto()
                    {

                    },
                    IsSuccess = false,
                    Message = "نام کاربری و رمز عبور را وارد نمایید"
                };
            }

            var user = _context.Users
                .Include(p => p.UserInRoles)
                .ThenInclude(p => p.Role)
                .Where(p => p.Email.Equals(UserName) && p.IsActive == true)
                .FirstOrDefault();
                
            if (user == null)
            {
                return new ResultDto<ResultUserLoginDto>()
                {
                    Data = new ResultUserLoginDto()
                    {

                    },
                    IsSuccess = false,
                    Message = "کاربری با این ایمیل در سایت فروشگاه امیر ثبت نام نکرده است"
                };
            }

            // all of 3 is same!
            //var passwordHasher = new PasswordHasher();
            //PasswordHasher passwordHasher = new PasswordHasher();
            PasswordHasher passwordHasher = new();
            bool resultVerifyPassword = passwordHasher.VerifyPassword(user.Password, Password);
            if (resultVerifyPassword == false)
            {
                return new ResultDto<ResultUserLoginDto>()
                {
                    Data = new ResultUserLoginDto()
                    {

                    },
                    IsSuccess = false,
                    Message = "رمز وارد شده اشتباه است!",

                };
            }

            var Roles = "";
            foreach (var item in user.UserInRoles)
            {
                Roles += $"{item.Role.Name}";
            }

            return new ResultDto<ResultUserLoginDto>()
            {
                Data = new ResultUserLoginDto()
                {
                    Name = user.FullName,
                    Roles = Roles,
                    UserId = user.Id
                },
                IsSuccess = true,
                Message = "ورود به سایت فروشگاه با موفقیت انجام شد."
            };
        }
    }

    public class ResultUserLoginDto
    {
        public long UserId { get; set; }
        public string Roles { get; set; }
        public string Name { get; set; }
    }
}
