using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class SyncLog
    {
        public int Id { get; set; }
        public Guid TableId { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
    }
}
