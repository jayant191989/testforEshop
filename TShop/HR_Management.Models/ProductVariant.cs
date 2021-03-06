﻿using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class ProductVariant : AuditableEntity<Guid>
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? MRP { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
