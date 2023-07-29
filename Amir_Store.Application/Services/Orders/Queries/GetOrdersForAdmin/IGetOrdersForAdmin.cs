using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using Amir_Store.Domain.Entities.Finance;
using Amir_Store.Domain.Entities.Orders;
using Amir_Store.Domain.Entities.Products;
using Amir_Store.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Amir_Store.Application.Services.Orders.Queries.GetOrdersForAdmin
{
    public interface IGetOrdersForAdmin
    {
        ResultDto<List<OrdersDto>> Execute(OrderState orderState);
    }

    public class GetOrdersForAdmin : IGetOrdersForAdmin
    {
        private readonly IDataBaseContext _context;

        public GetOrdersForAdmin(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<OrdersDto>> Execute(OrderState orderState)
        {
            var orders = _context.Orders
                .Include(p => p.OrderDetails)
                .Where(p => p.OrderState == orderState)
                .OrderByDescending(p => p.Id)
                .ToList()
                .Select(p => new OrdersDto
                {
                    InsetTime = p.InsertTime,
                    OrderId = p.Id,
                    OrderState = p.OrderState,
                    ProductCount = p.OrderDetails.Count(),
                    RequestId = p.RequestPayId,
                    UserId = p.UserId,
                }).ToList();

            return new ResultDto<List<OrdersDto>>()
            {
                Data = orders,
                IsSuccess = true,
            };
        }
    }

    public class OrdersDto
    {
        public long OrderId { get; set; }
        public DateTime InsetTime { get; set; }
        public long RequestId { get; set; }
        public long UserId { get; set; }
        public OrderState OrderState { get; set; }
        public int ProductCount { get; set; }

    }
}
