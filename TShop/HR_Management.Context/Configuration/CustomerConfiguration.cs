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
    //public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    //{
    //    public CustomerConfiguration()
    //    {
    //        this.ToTable("tbl_Customer");
    //        Property(wo => wo.MedicalHistory).HasMaxLength(256).IsOptional();
    //      //  Property(wo => wo.Address).HasMaxLength(120).IsOptional();
    //        Property(p => p.Email).HasColumnAnnotation("Index",
    //                                    new IndexAnnotation(new IndexAttribute("AK_Customer", 1) { IsUnique = true }));

    //    }
    //}
}
