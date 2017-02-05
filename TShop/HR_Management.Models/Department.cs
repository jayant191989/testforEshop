using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class Department : AuditableEntity<Guid>
    {
        public Department()
        {
            this.Contacts = new List<Contact>();
        }

       
        public string Name { get; set; }
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public List<Contact> Contacts { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
