using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using Amir_Store.Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                //Category category = _context.Categories.Find(productDto.CategoryId);
                var category = _context.Categories.Find(productDto.CategoryId);
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

                List<ProductImage> productImages = new();
                foreach (var image in productDto.Images)
                {
                    var uploadResult = UploadFile(image);
                    productImages.Add(new ProductImage()
                    {
                        Product = product,
                        Src = uploadResult.FileNameAddress
                    });
                }
                _context.ProductImages.AddRange(productImages);

                List<ProductFeature> productFeatures = new();
                foreach (var item in productDto.ProductFeatures)
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
        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
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
        public List<AddNewProduct_Features> ProductFeatures { get; set; }

        public List<IFormFile> Images { get; set; }
    }

    public class AddNewProduct_Features
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
    public class UploadDto
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }
}
