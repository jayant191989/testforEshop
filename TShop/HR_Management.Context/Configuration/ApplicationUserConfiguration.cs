
using HR_Management.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
       public ApplicationUserConfiguration()
       {
           Property(au => au.FirstName).HasMaxLength(15).IsOptional();
           Property(au => au.LastName).HasMaxLength(15).IsOptional();         
           Ignore(au => au.RolesList);

       }
    }
}
