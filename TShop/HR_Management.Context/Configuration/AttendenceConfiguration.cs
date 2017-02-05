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
    public class AttendenceConfiguration : EntityTypeConfiguration<Attendence>
    {
        public AttendenceConfiguration()
        {
            this.ToTable("tbl_Attendence");
            this.HasKey(t => t.Id);
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Date).HasColumnAnnotation("Index",
                                       new IndexAnnotation(new IndexAttribute("AK_Attendence_Date") { IsUnique = true }));

        }
    }
}
