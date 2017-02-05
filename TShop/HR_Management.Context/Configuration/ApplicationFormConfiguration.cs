using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
    public class ApplicationFormConfiguration : EntityTypeConfiguration<ApplicationForm>
    {
       public ApplicationFormConfiguration()
        {
            this.ToTable("tbl_ApplicationForm");
            HasRequired(d => d.Company).WithMany().WillCascadeOnDelete(false);
        }
    }
}
