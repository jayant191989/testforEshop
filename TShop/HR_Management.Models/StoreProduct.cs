using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class StoreProduct : AuditableEntity<Guid>
    {
        public string BatchNumber { get; set; }
        public DateTime ProductEnterDate { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal? CostPricePerUnit { get; set; }
        public decimal? DiscountRatePerUnit { get; set; }
        public decimal? MRPPerUnit { get; set; }      
        public decimal? Quantity { get; set; }
      
        public Guid BatchId { get; set; }
        public virtual Batch Batch { get; set; }
        public virtual IList<DailyItem> DailyItems { get; set; }
    }
}
