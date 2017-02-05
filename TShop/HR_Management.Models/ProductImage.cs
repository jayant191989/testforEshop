using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class ProductImage : AuditableEntity<Guid>
    {
        public string FullName { get; set; }
        public string ImageName { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public string Extention { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
