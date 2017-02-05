using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HR_Management.Models
{
    public class Membership : AuditableEntity<Guid>
    {
        public string MembershipName { get; set; }
        public DateTime? SchemeStartDate { get; set; }
        public DateTime? SchemeEndDate { get; set; }
        public string TimePeriod { get; set; }
        public decimal Discount { get; set; }
        public decimal? Fees { get; set; }

      
        public List<EnrollCustomer> EnrollCustomers { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
