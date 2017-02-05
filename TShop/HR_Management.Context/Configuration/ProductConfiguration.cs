using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
   public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            this.ToTable("tbl_Product");
            //Property(p => p.CompanyName).HasColumnAnnotation("Index",
            //                            new IndexAnnotation(new IndexAttribute("AK_Company_CompanyName") { IsUnique = true }));
            
        }
    }
}
