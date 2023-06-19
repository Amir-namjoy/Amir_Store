using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common;
using Amir_Store.Common.Dto;
using Amir_Store.Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Amir_Store.Application.Services.Products.Commands.AddNewProduct
{
    public interface IAddNewProductService
    {
        public ResultDto Execute(RequestAddNewProductDto productDto);
    }
    public class AddNewProductService : IAddNewProductService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddNewProductService(IDataBaseContext dataBaseContext, IHostingEnvironment hostingEnvironment)
        {
            _context = dataBaseContext;
            _environment = hostingEnvironment;
        }

        public ResultDto Execute(RequestAddNewProductDto productDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productDto.Name))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "لطفاً نام محصول را وارد کنید"
                    };
                }
                //var category = _context.Categories.Find(productDto.CategoryId);
                Category category = _context.Categories.Find(productDto.CategoryId);
                Product product = new()
                {
                    Brand = productDto.Brand,
                    Description = productDto.Description,
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Inventory = productDto.Inventory,
                    Category = category,
                    Displayed = productDto.Displayed,
                };
                _context.Products.Add(product);

                List<ProductImage> productImages = new List<ProductImage>();
                foreach (var image in productDto.Images)
                {
                    string folder = $@"images\ProductImages\";
                    var uploadResult = UploadFile.Execute(image, folder, _environment);
                    productImages.Add(new ProductImage()
                    {
                        Product = product,
                        //ProductId = product.Id,
                        Src = uploadResult.FileNameAddress
                    });
                }
                _context.ProductImages.AddRange(productImages);

                List<ProductFeature> productFeatures = new();
                foreach (var item in productDto.Features)
                {
                    productFeatures.Add(new ProductFeature()
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product,
                    });
                }
                _context.ProductFeatures.AddRange(productFeatures);
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = $"محصول {productDto.Name} با موفقیت اضافه شد."
                };
            }
            catch
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "افزودن محصول با خطا مواجه و انجام نشد!"
                };
            }
        }
       
    }

    public class RequestAddNewProductDto
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }

        public long CategoryId { get; set; }
        public List<AddNewProduct_Features> Features { get; set; }

        public List<IFormFile> Images { get; set; }
    }

    public class AddNewProduct_Features
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
    
}
