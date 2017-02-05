using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
    public class EmployeeSalaryConfiguration : EntityTypeConfiguration<EmployeeSalary>
    {
       public EmployeeSalaryConfiguration()
        {
            this.ToTable("tbl_EmployeeSalary");
            Property(p => p.WorkHourTotal).HasPrecision(18, 2).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(p => p.OverTimeTotal).HasPrecision(18, 2).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
           
        }
    }
}
