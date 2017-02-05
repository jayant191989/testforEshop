using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class ProductsAttribute : AuditableEntity<Guid>
    {
        public ProductsAttribute()
        {
            ProductCategories = new List<ProductCategory>();          
        }
        public string Name { get; set; }
        public virtual IList<ProductAttributeOptions> ProductAttributeOptions { get; set; }
        public virtual IList<Product> Products { get; set; }
        public virtual IList<ProductCategory> ProductCategories { get; set; }

    }
}
