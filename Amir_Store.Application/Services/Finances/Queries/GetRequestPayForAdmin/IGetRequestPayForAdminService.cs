using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using Microsoft.EntityFrameworkCore;

namespace Amir_Store.Application.Services.Finances.Queries.GetRequestPayForAdmin
{
    public interface IGetRequestPayForAdminService
    {
        ResultDto<List<RequestPayDto>> Execute();
    }

    public class GetRequestPayForAdminService : IGetRequestPayForAdminService
    {
        private readonly IDataBaseContext _context;

        public GetRequestPayForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<RequestPayDto>> Execute()
        {
            var requestPays = _context.RequestPays
                .Include(p => p.User)
                .ToList()
                .Select(p => new RequestPayDto
                {
                    UserId = p.UserId,
                    Amount = p.Amount,
                    Authority = p.Authority,
                    Guid = p.Guid,
                    Id = p.Id,
                    IsPay = p.IsPay,
                    PayDate = p.PayDate,
                    RefId = p.RefId,
                    UserName = p.User.FullName,
                }).ToList();
            return new ResultDto<List<RequestPayDto>>()
                {
                    Data = requestPays,
                    IsSuccess = true,
                    Message = "",
                };
        }
    }

    public class RequestPayDto
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
    }
}
