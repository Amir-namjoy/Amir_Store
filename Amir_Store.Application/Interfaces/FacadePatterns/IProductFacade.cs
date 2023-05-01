using Amir_Store.Application.Services.Products.Commands.AddNewCategory;
using Amir_Store.Application.Services.Products.Commands.AddNewProduct;
using Amir_Store.Application.Services.Products.Queries.GetAllCategories;
using Amir_Store.Application.Services.Products.Queries.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Interfaces.FacadePatterns
{
    public interface IProductFacade
    {
        AddNewCategoryService AddNewCategoryService { get; }
        IGetCategoriesService GetCategoriesService { get; }
        AddNewProductService AddNewProductService { get; }
        IGetAllCategoriesService GetAllCategoriesService { get; }

    }
}
