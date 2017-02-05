using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.ShoppingCartBL.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }

        [Display(Name = "Mobile no")]
        [Required(ErrorMessage = "Please enter mobile no")]
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ClientRegisterViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string RegisterName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string RegisterEmail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RegisterPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("RegisterPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string RegisterConfirmPassword { get; set; }
    }
}
