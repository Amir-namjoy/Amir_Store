using Amir_Store.Application.Services.Common.Queries.GetHomePageImages;
using Amir_Store.Application.Services.Common.Queries.GetSliders;
using Amir_Store.Application.Services.Products.Queries.GetProductForSite;
using System.Collections.Generic;

namespace EndPoint.Site.Models.HomePage
{
    public class HomePageViewModel
    {
        public List<SliderDto> Sliders { get; set; }
        public List<HomePageImageDto> PageImages { get; set; }
        public List<ProductForSiteDto> Camera { get; set; }
        public List<ProductForSiteDto> Mobile { get; set; }
        public List<ProductForSiteDto> HomeAppliances { get; set; }
        public List<ProductForSiteDto> Laptop { get; set; }
    }
}
