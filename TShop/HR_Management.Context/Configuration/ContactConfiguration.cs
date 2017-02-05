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
    public class ContactConfiguration : EntityTypeConfiguration<Contact>
    {
        public ContactConfiguration()
        {
            this.ToTable("tbl_Contact");
            HasRequired(d => d.Department).WithMany(e => e.Contacts).WillCascadeOnDelete(false);
            HasRequired(d => d.Branch).WithMany(e => e.Contacts).WillCascadeOnDelete(false);
          //  HasRequired(d => d.Company).WithMany().WillCascadeOnDelete(false);
            Property(e => e.Email).HasMaxLength(50).IsRequired();
            Property(e => e.BranchId).IsRequired();
            Property(p => p.Email).HasColumnAnnotation("Index",
                                        new IndexAnnotation(new IndexAttribute("AK_Employee") { IsUnique = true }));
        }
    }
}
