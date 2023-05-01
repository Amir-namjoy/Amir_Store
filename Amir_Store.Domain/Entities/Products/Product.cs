using Amir_Store.Domain.Entities.Commons;
using Amir_Store.Domain.Entities.Products;
using System.Collections.Generic;

namespace Amir_Store.Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }

        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }
    }
}
