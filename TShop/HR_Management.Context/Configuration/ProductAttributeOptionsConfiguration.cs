using HR_Management.Model.Common;
using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
    public class ProductAttributeOptionsConfiguration : EntityTypeConfiguration<ProductAttributeOptions>
    {
        public ProductAttributeOptionsConfiguration()
        {
            this.ToTable("tbl_ProductAttributeOptions");
           // HasRequired(d => d.ProductsAttribute).WithMany().WillCascadeOnDelete(true);
        }
    }
}
