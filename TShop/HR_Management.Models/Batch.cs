using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class Batch : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public DateTime EnteredDate { get; set; }       
        public virtual IList<StoreProduct> StoreProducts { get; set; }
        public Guid StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
