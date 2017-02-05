using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Model.Models
{
    public class State : Entity<Guid>
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is Country")]
        public Guid CountryId { get; set; }


        public virtual Country Country { get; set; }
    }
}
