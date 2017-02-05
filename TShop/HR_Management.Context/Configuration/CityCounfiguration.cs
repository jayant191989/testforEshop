using HR_Management.Model.Models;
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
    public class CityCounfiguration : EntityTypeConfiguration<City>
    {
        public CityCounfiguration()
        {
            this.ToTable("tbl_City");

            Property(c => c.Code)
                .HasMaxLength(10);
        }
    }
}
