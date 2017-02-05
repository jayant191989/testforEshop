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
    public class UserOrderConfiguration : EntityTypeConfiguration<UserOrder>
    {
        public UserOrderConfiguration()
        {
            this.ToTable("tbl_UserOrder");
            //HasRequired(d => d.Company).WithMany().WillCascadeOnDelete(false);
            //HasRequired(d => d.Branch).WithMany(e => e.Departments).WillCascadeOnDelete(false);
            //Property(d => d.Name).HasMaxLength(20).IsRequired();
            Property(d => d.InvoiceNo).HasMaxLength(20).IsRequired().HasColumnAnnotation("Index",
                                       new IndexAnnotation(new IndexAttribute("UK_OrderInvoice") { IsUnique = true }));
           
        }
    }
}
