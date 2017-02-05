using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
    public class DailyItemConfiguration : EntityTypeConfiguration<DailyItem>
    {
        public DailyItemConfiguration()
        {
            this.ToTable("tbl_DailyItems");
            HasRequired(d => d.StoreProduct).WithMany(e => e.DailyItems).WillCascadeOnDelete(false);            
        }
    }
}
