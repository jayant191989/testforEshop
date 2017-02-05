using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
      public CompanyConfiguration()
        {
            this.ToTable("tbl_Company");
            Property(p => p.CompanyName).HasColumnAnnotation("Index",
                                        new IndexAnnotation(new IndexAttribute("AK_Company_CompanyName") { IsUnique = true }));
            Property(wo => wo.CompanyName).HasMaxLength(20).IsOptional();     
        }
    }
}
