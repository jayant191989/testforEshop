using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class CompanyViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "You must enter an Company Name.")]
        [StringLength(20, ErrorMessage = "The account number must be 20 characters or shorter.")]
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}