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
using static System.Net.Mime.MediaTypeNames;

namespace Amir_Store.Application.Services.HomePage.AddNewSlider
{
    public interface IAddNewSliderService
    {
        ResultDto Execute(IFormFile file, string link);
    }

    public class AddNewSliderService : IAddNewSliderService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddNewSliderService(IHostingEnvironment environment, IDataBaseContext context)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(IFormFile file, string link)
        {
            string folder = $@"images\HomePage\Sliders\";
            var uploadResult = UploadFile.Execute(file, folder, _environment);
            Slider slider = new Slider() 
            { 
                Link = link,
                Src = uploadResult.FileNameAddress,

            };
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return new ResultDto { IsSuccess = true };
        }
    }
}
