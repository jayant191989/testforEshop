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
    public class SalaryConfiguration : EntityTypeConfiguration<Salary>
    {
        public SalaryConfiguration()
        {
            this.ToTable("tbl_Salary");
            Property(p => p.Date).IsOptional().HasColumnAnnotation("Index",
                                       new IndexAnnotation(new IndexAttribute("AK_Salary_Date") { IsUnique = true }));

            HasRequired(d => d.Company).WithMany().WillCascadeOnDelete(false);
        }
    }
}
