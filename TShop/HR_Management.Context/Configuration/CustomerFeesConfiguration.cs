using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
    public class CustomerFeesConfiguration : EntityTypeConfiguration<CustomerFees>
    {
        public CustomerFeesConfiguration()
        {
            this.ToTable("tbl_CustomerFees");
            Property(wo => wo.DueFees).HasPrecision(18, 2);
            HasRequired(wo => wo.EnrollCustomer).WithMany(au => au.CustomerFees).WillCascadeOnDelete(false);
        }
    }
}
