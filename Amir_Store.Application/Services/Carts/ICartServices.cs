using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using Amir_Store.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Services.Carts
{
    public interface ICartServices
    {
        ResultDto AddToCart(long ProductId, Guid BrowserId);
        ResultDto RemoveFromCart(long ProductId, Guid BrowserId);
        ResultDto<CartDto> GetMyCart(Guid BrowserId, long? UserId);
        ResultDto Add(long CartItemId);
        ResultDto LowOff(long CartItemId);
    }

    public class CartService : ICartServices
    {
        private readonly IDataBaseContext _context;
        public CartService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto AddToCart(long ProductId, Guid BrowserId)
        {
            var cart = _context.Carts.Where(p => p.BrowserId == BrowserId && p.Finished == false).FirstOrDefault();
            if (cart == null)
            {
                Cart newCart = new Cart()
                {
                    Finished = false,
                    BrowserId = BrowserId,
                };
                _context.Carts.Add(newCart);
                _context.SaveChanges();
                cart = newCart;
            }
            var product = _context.Products.Find(ProductId);
            var cartItem = _context.CartItems.Where(p => p.ProductId == ProductId && p.CartId == cart.Id).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.Count++;
            }
            else
            {
                CartItem newCartItem = new CartItem()
                {
                    Cart = cart,
                    Count = 1,
                    Price = product.Price,
                    Product = product,
                };
                _context.CartItems.Add(newCartItem);
                _context.SaveChanges();
            }
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"محصول {product.Name} با موفقیت به سبد خرید شما اضافه شد.",
            };
        }

        public ResultDto<CartDto> GetMyCart(Guid BrowserId, long? UserId)
        {
            var cart = _context.Carts
                .Include(p => p.CartItems)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductImages)
                .Where(p => p.BrowserId == BrowserId && p.Finished == false)
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            if(UserId != null)
            {
                var user = _context.Users.Find(UserId);
                cart.User = user;
                _context.SaveChanges();
            }
            return new ResultDto<CartDto>()
            {
                Data = new CartDto()
                {
                    ProductCount = cart.CartItems.Count,
                    SumAmount = cart.CartItems.Sum(p => p.Price * p.Count),
                    CartItems = (List<CartItemDto>)cart.CartItems.Select(p => new CartItemDto
                    {
                        Count = p.Count,
                        Price = p.Price,
                        Product = p.Product.Name,
                        Id = p.Id,
                        Image = p.Product?.ProductImages?.FirstOrDefault()?.Src ?? "",
                    }).ToList(),
                },
                IsSuccess = true,
            };
        }

        public ResultDto Add(long cartItemId)
        {
            var cartItem = _context.CartItems.Find(cartItemId);
            cartItem.Count++;
            _context.SaveChanges();
            return new ResultDto() { IsSuccess = true };
        }
        public ResultDto LowOff(long cartItemId)
        {
            var cartItem = _context.CartItems.Find(cartItemId);
            if (cartItem.Count <= 0)
            {
                return new ResultDto() { IsSuccess = false };
            }
            cartItem.Count--;
            _context.SaveChanges();
            return new ResultDto() { IsSuccess = true };
        }

        public ResultDto RemoveFromCart(long ProductId, Guid BrowserId)
        {
            var cartItem = _context.CartItems.Where(p => p.Cart.BrowserId == BrowserId).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.IsRemoved = true;
                cartItem.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "محصول از سبد خرید شما حذف شد",
                };
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد",
                };
            }
        }
    }

    public class CartDto
    {
        public int ProductCount { get; set; }
        public int SumAmount { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class CartItemDto
    {
        public long Id { get; set; }
        public string Product { set; get; }
        public string Image { set; get; }
        public int Count { set; get; }
        public int Price { set; get; }
    }
}