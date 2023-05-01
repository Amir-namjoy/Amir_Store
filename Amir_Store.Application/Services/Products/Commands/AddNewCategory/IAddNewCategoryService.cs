using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using Amir_Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Amir_Store.Application.Services.Products.Commands.AddNewCategory
{
    public interface IAddNewCategoryService
    {
        ResultDto Execute(long? ParentId, string Name);

    }

    public class AddNewCategoryService : IAddNewCategoryService
    {
        private readonly IDataBaseContext _context;
        public AddNewCategoryService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long? ParentId, string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفاً نام دسته بندی را وارد کنید"
                };

            }
            Category category = new()
            {
                Name = Name,
                ParentCategory = GetParent(ParentId)

            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"دسته بندی {Name} اضافه شد"
            };

        }

        private Category GetParent(long? ParentId)
        {
            return _context.Categories.Find(ParentId);
        }
    }
}
