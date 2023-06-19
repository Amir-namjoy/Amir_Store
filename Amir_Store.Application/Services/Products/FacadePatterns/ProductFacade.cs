using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Application.Interfaces.FacadePatterns;
using Amir_Store.Application.Services.Products.Commands.AddNewCategory;
using Amir_Store.Application.Services.Products.Commands.AddNewProduct;
using Amir_Store.Application.Services.Products.Queries.GetAllCategories;
using Amir_Store.Application.Services.Products.Queries.GetCategories;
using Amir_Store.Application.Services.Products.Queries.GetProductDetailForAdmin;
using Amir_Store.Application.Services.Products.Queries.GetProductsForAdmin;
using Amir_Store.Application.Services.Products.Queries.GetProductForSite;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amir_Store.Application.Services.Products.Queries.GetProductDetailForSite;

namespace Amir_Store.Application.Services.Products.FacadePatterns
{
    public class ProductFacade : IProductFacade
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public ProductFacade(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private IAddNewCategoryService _addNewCategoryService;
        public IAddNewCategoryService AddNewCategoryService
        { 
            get
            {
                //return _addNewCategoryService = _addNewCategoryService ?? new AddNewCategoryService(_context);
                return _addNewCategoryService ??= new AddNewCategoryService(_context);
            }
        }

        private GetCategoriesService _getCategoriesService;
        public IGetCategoriesService GetCategoriesService 
        {
            get
            {
                return _getCategoriesService ??= new GetCategoriesService(_context);
            }
        }

        private AddNewProductService _addNewProductService;
        public AddNewProductService AddNewProductService
        {
            get
            {
                return _addNewProductService ??= new AddNewProductService(_context, _environment);
            }
        }

        private IGetAllCategoriesService _getAllCategoriesService;
        public IGetAllCategoriesService GetAllCategoriesService
        {
            get 
            { 
                return _getAllCategoriesService ??= new GetAllCategoriesService(_context);
            }
        }

        private IGetProductForAdminServices _getProductForAdminServices;
        public IGetProductForAdminServices GetProductForAdminServices
        {
            get
            {
                return _getProductForAdminServices ??= new GetProductForAdminServices(_context);
            }
        }

        private IGetProductDetailForAdminService _getProductDetailForAdminService;
        public IGetProductDetailForAdminService GetProductDetailForAdminService
        {
            get
            {
                return _getProductDetailForAdminService ??= new GetProductDetailForAdminService(_context);
            }
        }

        private IGetProductForSiteService _getProductForSiteService;
        public IGetProductForSiteService GetProductForSiteService
        {
            get
            {
                return _getProductForSiteService ??= new GetProductForSiteService(_context);
            }
        }
        
        private IGetProductDetailForSiteService _getProductDetailForSiteService;
        public IGetProductDetailForSiteService GetProductDetailForSiteService
        {
            get
            {
                return _getProductDetailForSiteService ??= new GetProductDetailForSiteService(_context);
            }
        }
    }
}
