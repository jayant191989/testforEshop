using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
    public class MembershipConfiguration : EntityTypeConfiguration<Membership>
    {
        public MembershipConfiguration()
        {
            this.ToTable("tbl_Membership");
            Property(wo => wo.Discount).HasPrecision(18, 2);
            Property(wo => wo.Fees).HasPrecision(18, 2);
        }
    }
}
