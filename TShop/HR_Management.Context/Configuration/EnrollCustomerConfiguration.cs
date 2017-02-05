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
    public class EnrollCustomerConfiguration : EntityTypeConfiguration<EnrollCustomer>
    {
        public EnrollCustomerConfiguration()
        {
            this.ToTable("tbl_EnrollCustomer");
           // Property(p => p.Email).HasColumnAnnotation("Index",
                                      //   new IndexAnnotation(new IndexAttribute("AK_EnrollCustomer", 1) { IsUnique = true }));
        }

    }
}
