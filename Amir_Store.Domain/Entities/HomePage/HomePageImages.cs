using Amir_Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Domain.Entities.HomePage
{
    public class HomePageImage:BaseEntity
    {
        public string Src { get; set; }
        public string link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }

    public enum ImageLocation
    {
        L1 = 0,
        L2 = 1,
        R1 = 2,
        CenterFullScreen = 3,
        G1=5, 
        G2=6,
    }
}
