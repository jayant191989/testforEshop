using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Context.Configuration
{
    public class ParticularConfiguration : EntityTypeConfiguration<Particular>
    {
        public ParticularConfiguration()
        {
            this.ToTable("tbl_Particular");
        }
    }
}
