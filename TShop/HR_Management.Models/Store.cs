using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class Store : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public virtual IList<Batch> Batches { get; set; }
    }
}
