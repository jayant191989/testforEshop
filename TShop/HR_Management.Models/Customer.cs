using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Models
{
    //public class Customer : AuditableEntity<Guid>
    //{       
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public int? Age { get; set; }
    //    public string Gender { get; set; }
    //    public DateTime? DateOfBirth { get; set; }
    //    public string FathersName { get; set; }
    //    public string CustomerType { get; set; }


    //    public string HouseNumber { get; set; }
    //    public string StreetName { get; set; }
    //    public string LandMark { get; set; }
    //    public string Colony { get; set; }
    //    public string City { get; set; }
    //    public string State { get; set; }
    //    public string ZipCode { get; set; }

    //    [Required(ErrorMessage = "You must enter a Email.")]
    //    [StringLength(80)]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }
    //    public string Mobile { get; set; }
    //    public string EmpergencyContact { get; set; }


    //    public bool? Acloholic { get; set; }
    //    public bool? Smoke { get; set; }
    //    public string MedicalHistory { get; set; }
    //    public string Weight { get; set; }
    //    public string Height { get; set; }


    //    public bool? PersenalTrainer { get; set; }
    //    public bool? GymJoinedBefore { get; set; }
    //    public DateTime? DateOfJoining { get; set; }
    //    public string TrainingGoal { get; set; }
    //    public string GymReference { get; set; }
    //    public string MainImageName { get; set; }
    //    public string ImageExtention { get; set; }

    //    [Display(Name = "Full Name")]
    //    public string FullName
    //    {
    //        get { return FirstName + " " + LastName; }
    //    }

    //    public string AddressBlock
    //    {
    //        get
    //        {
    //            string addressBlock = string.Format("{0}<br/>{1}, {2}, {3} ,{4} </br> {5} ,{6} ,{7}", FullName, HouseNumber, StreetName, LandMark, Colony, City, State, ZipCode).Trim();
    //            return addressBlock == "<br/>," ? string.Empty : addressBlock;
    //        }
    //    }
    //    public bool? Status { get; set; }
    //    public Guid CompanyId { get; set; }
    //    public virtual Company Company { get; set; }

    //}
}
