using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Model.Models
{
    public class Country :Entity<Guid>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Code { get; set; }      
        public virtual IList<State> States { get; set; }

    }
}
