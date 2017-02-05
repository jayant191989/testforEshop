using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class BranchViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a Code.")]
        [StringLength(20)]
        public string BranchCode { get; set; }

        [Required(ErrorMessage = "You must enter a Name.")]
        [StringLength(20)]
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }      
        public int CompanyId { get; set; }
      
    }
}