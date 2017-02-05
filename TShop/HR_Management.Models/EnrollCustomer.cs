using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    public class EnrollCustomer : AuditableEntity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FathersName { get; set; }

        [Required(ErrorMessage = "You must enter a Email.")]
        [StringLength(80)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? MembershipEndTime { get; set; }

        public DateTime? EnrolledDate { get; set; }
        public DateTime? DueTime { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public List<CustomerFees> CustomerFees { get; set; }

        public Guid MembershipId { get; set; }
        public virtual Membership Membership { get; set; }
    

    }
}
