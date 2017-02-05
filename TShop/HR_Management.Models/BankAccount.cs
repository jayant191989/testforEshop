using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class BankAccount : AuditableEntity<Guid>
    {
        public string BankName { get; set; }
        public string IFSECode { get; set; }

        [Display(Name = "Bank Name")]
        [Required(ErrorMessage = "You must enter an Bank Name.")]
        [StringLength(20, ErrorMessage = "The account number must be 20 characters or shorter.")]
        public string AccountNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
