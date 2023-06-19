using Amir_Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amir_Store.Domain.Entities.HomePage
{
    public class Slider:BaseEntity
    {
        public string Src { get; set; }
        public string Link { get; set; }
        public int Click { get; set; }
    }
}
