using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.ViewModels
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }


        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LandMark { get; set; }
        public string Colony { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public string Gender { get; set; }
        public List<SelectListItem> getAllGenderList { get; set; }
        public List<SelectListItem> getGenderList()
        {
            List<SelectListItem> myList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="1",Text="Male"},
                 new SelectListItem{ Value="2",Text="Female"},
              
             };
            myList = data.ToList();
            return myList;
        }
        public DateTime? DateOfBirth { get; set; }
        public string FathersName { get; set; }
        public string Anniversary { get; set; }
        public string DateOfResignation { get; set; }


        [Required(ErrorMessage = "You must enter a Email.")]
        [StringLength(80)]
        [Display(Name = "Email")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "E-Mail is not in proper format")]
        public string Email { get; set; }

        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "PhoneNumber should contain only numbers")]
        public string Mobile { get; set; }
        public string EmpergencyContact { get; set; }


        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string AddressBlock
        {
            get
            {
                string addressBlock = string.Format("{0}<br/>{1}, {2}, {3} ,{4} </br> {5} ,{6} ,{7}", FullName, HouseNumber, StreetName, LandMark, Colony, City, State, ZipCode).Trim();
                return addressBlock == "<br/>," ? string.Empty : addressBlock;
            }
        }
        public Guid Options
        {
            //get
            //{
            //    string edit = string.Format("/Employees/Edit/{0}", Id);
            //    return edit;
            //}
            get
            {
                Guid edit = Id;
                return edit;
            }
        }
        public DateTime? JoinDate { get; set; }
        [Display(Name = "Branch Name")]
        public Guid BranchId { get; set; }

        [Display(Name = "Department Name")]
        public Guid DepartmentID { get; set; }
      
        public Guid CompanyId { get; set; }

        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }
    }
}