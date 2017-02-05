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
    public class BankAccountConfiguration : EntityTypeConfiguration<BankAccount>
    {
        public BankAccountConfiguration()
        {
            this.ToTable("tbl_BankAccount");
            Property(p => p.AccountNumber).IsOptional().HasColumnAnnotation("Index",
                                       new IndexAnnotation(new IndexAttribute("AK_BankAccount_AccountNumber") { IsUnique = true }));

            HasRequired(d => d.Contact).WithMany(e => e.BankAccounts).WillCascadeOnDelete(false);
            HasRequired(d => d.Company).WithMany().WillCascadeOnDelete(false);
        }
    }
}
