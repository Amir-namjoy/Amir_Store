using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common;
using Amir_Store.Common.Dto;
using Amir_Store.Domain.Entities.HomePage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Services.HomePage.AddHomePageImages
{
    public interface IAddHomePageImagesService
    {
        ResultDto Execute(requestAddHomePageImage request);
    }
    public class AddHomePageImagesService : IAddHomePageImagesService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddHomePageImagesService(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(requestAddHomePageImage request)
        {
            string folder = $@"images\HomePage\Images\";
            var upLoadResult = UploadFile.Execute(request.file, folder, _environment);
            var homePageImage = new HomePageImage()
            {
                Src = upLoadResult.FileNameAddress,
                link = request.link,
                ImageLocation = request.imageLocation
            };
            _context.HomePageImages.Add(homePageImage);
            _context.SaveChanges();
            return new ResultDto() { IsSuccess = true };
        }


    }
    public class requestAddHomePageImage
    {
        public IFormFile file { get; set; }
        public string link { get; set; }
        public ImageLocation imageLocation { get; set; }
    }

}
