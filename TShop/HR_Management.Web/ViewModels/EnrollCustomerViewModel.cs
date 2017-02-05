using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class EnrollCustomerViewModel
    {
        public int? Id { get; set; }
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
        [Required]
        public DateTime? DateOfJoining { get; set; }
        public DateTime? MembershipEndTime { get; set; }
        [Required]
        public DateTime? EnrolledDate { get; set; }
        public DateTime? DueTime { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public Guid MembershipId { get; set; }

    }
}