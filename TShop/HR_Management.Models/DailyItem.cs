using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class DailyItem : AuditableEntity<Guid>
    {
        public decimal? DiscountRatePerUnit { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? ItemAmount { get; set; }
        public Guid StoreProductId { get; set; }
        public virtual StoreProduct StoreProduct { get; set; }
        public Guid DailyId { get; set; }
        public virtual Daily Daily { get; set; }
    }
}
