using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using Amir_Store.Domain.Entities.Orders;
using Amir_Store.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Amir_Store.Application.Services.Orders.Queries.GetUserOrders
{
    public interface IGetUserOrdersService
    {
        ResultDto<List<GetUserOrderDto>> Execute(long userId);
    }

    public class GetUserOrdersService : IGetUserOrdersService
    {
        private readonly IDataBaseContext _context;

        public GetUserOrdersService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<GetUserOrderDto>> Execute(long userId)
        {
            var orders = _context.Orders
                .Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Id).ToList().Select(p => new GetUserOrderDto
                {
                    OrderId = p.Id,
                    OrderState = p.OrderState,
                    RequestPayId = p.RequestPayId,
                    OrderDetails = p.OrderDetails.Select(o => new OrderDetailDto
                    {
                        Count = o.Count,
                        OrderDetailId = o.Id,
                        Price = o.Price,
                        ProductId = o.ProductId,
                        ProductName = o.Product.Name,
                    }).ToList(),
                }).ToList();

            return new ResultDto<List<GetUserOrderDto>>()
            {
                Data = orders,
                IsSuccess = true,
                Message = "",
            };
        }
    }

    public class GetUserOrderDto
    {
        public long OrderId { get; set; }
        public OrderState OrderState { get; set; }
        public long RequestPayId { get; set; }
        public string Address { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }

    public class OrderDetailDto
    {
        public long OrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
