using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.ViewModels
{
    public class AttendenceViewModel
    {
        public DateTime Date { get; set; }      
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}