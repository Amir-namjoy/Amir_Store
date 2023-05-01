using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Services.Products.Queries.GetAllCategories
{
    public interface IGetAllCategoriesService
    {
        ResultDto<List<CategoryDto>> Execute();
    }


    public class GetAllCategoriesService : IGetAllCategoriesService
    {
        private readonly IDataBaseContext _context;

        public GetAllCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CategoryDto>> Execute()
        {
            try
            {
                var categories = _context
                    .Categories
                    .Include(p => p.ParentCategory)
                    .Where(p => p.ParentCategoryId != null)
                    .ToList() // Test if Remove is any error
                    .Select(p => new CategoryDto
                    {
                        Id = p.Id,
                        Name = $"{p.ParentCategory.Name} - {p.Name}",
                    }
                    ).ToList();

                return new ResultDto<List<CategoryDto>>
                {
                    Data = categories,
                    IsSuccess = true,
                    Message = "دسته بندی ها با موفقیت خوانده شد.",
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<List<CategoryDto>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }

    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }



}
