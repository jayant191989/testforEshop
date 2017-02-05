using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
    public class BatchConfiguration : EntityTypeConfiguration<Batch>
    {
        public BatchConfiguration()
        {
            this.ToTable("tbl_Batch");
        }
    }
}
