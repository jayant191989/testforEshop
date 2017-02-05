using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
   public class Daily : AuditableEntity<Guid>
    {
        public string HeadName { get; set; }
        public decimal Invoice { get; set; }
        public DateTime Date { get; set; }
        public Guid ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public Guid ParticularId { get; set; }
        public virtual Particular Particular { get; set; }
        public string Note { get; set; }
        public decimal ProductsTotal { get; set; }
        public decimal DailyTotal { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Net { get; set; }
        public decimal? Due { get; set; }
        public Guid? MembershipId { get; set; }
        public Guid? RefAccountId { get; set; }

        public virtual IList<DailyItem> DailyItems { get; set; }
    }
}
