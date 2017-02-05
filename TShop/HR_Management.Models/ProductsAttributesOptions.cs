using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class ProductAttributeOptions : AuditableEntity<Guid>
    {
        public ProductAttributeOptions()
        {
            Products = new List<Product>();
        }
        public string Name { get; set; }       
        public Guid ProductsAttributesId { get; set; }
        [ForeignKey("ProductsAttributesId")]
        public virtual ProductsAttribute ProductsAttribute { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
