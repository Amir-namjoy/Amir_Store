using Amir_Store.Application.Interfaces.Contexts;
using Amir_Store.Common.Dto;
using Amir_Store.Domain.Entities.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Application.Services.Common.Queries.GetHomePageImages
{
    public interface IGetHomePageImagesService
    {
        ResultDto<List<HomePageImageDto>> Execute();
    }
    public class GetHomePageImagesService : IGetHomePageImagesService
    {
        private readonly IDataBaseContext _context;
        public GetHomePageImagesService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<HomePageImageDto>> Execute()
        {
            var images = _context.HomePageImages.OrderByDescending(p => p.Id)
                .Select(p => new HomePageImageDto
                {
                    Id = p.Id,
                    Src = p.Src,
                    Link = p.link,
                    ImageLocation = p.ImageLocation,
                }).ToList();
            return new ResultDto<List<HomePageImageDto>>()
            { 
                Data = images,
                IsSuccess = true
            };
        }
    }

    public class HomePageImageDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
