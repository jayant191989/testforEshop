using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
   public class UserAddressConfiguration : EntityTypeConfiguration<UserAddress>
    {
        public UserAddressConfiguration()
        {
            this.ToTable("tbl_UserAddress");
            Property(au => au.AddressLine1).HasMaxLength(50).IsOptional();
            Property(au => au.AddressLine2).HasMaxLength(50).IsOptional();
            Property(au => au.City).HasMaxLength(30).IsOptional();
            Property(au => au.State).HasMaxLength(30).IsOptional();
            Property(au => au.State).HasMaxLength(30).IsOptional();
            Property(au => au.ZipCode).HasMaxLength(10).IsOptional();
        }
    }
}
