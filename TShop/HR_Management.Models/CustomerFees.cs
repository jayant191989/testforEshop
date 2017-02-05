using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class CustomerFees : AuditableEntity<Guid>
    {
        public decimal? DueFees { get; set; }
        public decimal SubmitFees { get; set; }
        public DateTime Date { get; set; }
        public Guid EnrollCustomerId { get; set; }
        public virtual EnrollCustomer EnrollCustomer { get; set; }
     

    }
}
