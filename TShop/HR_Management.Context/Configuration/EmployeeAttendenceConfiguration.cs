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
    public class EmployeeAttendenceConfiguration : EntityTypeConfiguration<EmployeeAttendence>
    {
       public EmployeeAttendenceConfiguration()
       {
           this.ToTable("tbl_EmployeeAttendence");
           Property(wo => wo.WorkHours).HasPrecision(18, 2);
           Property(wo => wo.OverTimeHours).HasPrecision(18, 2);
           Property(wo => wo.InTime).IsOptional();
           Property(wo => wo.OutTime).IsOptional();
       //    Property(ea => ea.Date).HasColumnAnnotation("Index",
                                      // new IndexAnnotation(new IndexAttribute("AK_EmployeeAttendence") { IsUnique = true }));
       }
    }
}
