using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using Amir_Store.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Services.Products.Queries.GetCategories
{
    public interface IGetCategoriesService
    {
        public ResultDto<List<CategoriesDto>> Execute(long? ParentId);
    }

    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDataBaseContext _context;
        public GetCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<CategoriesDto>> Execute(long? ParentId)
        {
            var Categories = _context.Categories
                .Include(p => p.ParentCategory)
                .Include(p => p.SubCategories)
                .Where(p => p.ParentCategoryId == ParentId)
                .ToList()
                .Select(p => new CategoriesDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Parent = p.ParentCategory != null ? new
                    ParentCategoryDto
                    {
                        Id = p.ParentCategory.Id,
                        Name = p.ParentCategory.Name,
                    }
                    : null,
                    //HasChild = p.SubCategories.Count > 0 ? true : false,
                    HasChild = p.SubCategories.Count > 0,
                }).ToList();
            return new ResultDto<List<CategoriesDto>>()
            {
                Data = Categories,
                IsSuccess = true,
                Message = "لیست دسته بندی ها با موفقیت خوانده شد"
            };
        }
    }

    public class CategoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasChild { get; set; }
        public ParentCategoryDto Parent { get; set; }

    }
    public class ParentCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
