using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class DepartmentViewModel
    {
        public DepartmentViewModel()
        {
            this.Contacts = new List<Contact>();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "You must enter a Name.")]
        [StringLength(20)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Name consist of letters only")]
        //[RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Name consist of letters only.")]
        public string Name { get; set; }

        [Display(Name = "Branch Name")]
        [Required]
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        [Display(Name = "Company Name")]
        [Required]
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public List<Contact> Contacts { get; set; }
        public string messegeToClient { get; set; }

    }
}