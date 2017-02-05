using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class Branch : AuditableEntity<Guid>
    {
        public string BranchCode { get; set; } 
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public List<Department> Departments { get; set; }
        public List<Contact> Contacts { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
