using Amir_Store.Application.Services.Products.Commands.AddNewCategory;
using Amir_Store.Application.Services.Products.Commands.AddNewProduct;
using Amir_Store.Application.Services.Products.Queries.GetAllCategories;
using Amir_Store.Application.Services.Products.Queries.GetCategories;
using Amir_Store.Application.Services.Products.Queries.GetProductDetailForAdmin;
using Amir_Store.Application.Services.Products.Queries.GetProductsForAdmin;
using Amir_Store.Application.Services.Products.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amir_Store.Application.Services.Products.Queries.GetProductDetailForSite;

namespace Amir_Store.Application.Interfaces.FacadePatterns
{
    public interface IProductFacade
    {
        //AddNewCategoryService AddNewCategoryService { get; }
        IAddNewCategoryService AddNewCategoryService { get; }
        IGetCategoriesService GetCategoriesService { get; }
        AddNewProductService AddNewProductService { get; }
        IGetAllCategoriesService GetAllCategoriesService { get; }
        /// <summary>
        /// دریافت لیست محصولات
        /// </summary>
        IGetProductForAdminServices GetProductForAdminServices { get; }
        IGetProductDetailForAdminService GetProductDetailForAdminService { get; }
        IGetProductForSiteService GetProductForSiteService { get; }
        IGetProductDetailForSiteService GetProductDetailForSiteService { get; }
    }
}
