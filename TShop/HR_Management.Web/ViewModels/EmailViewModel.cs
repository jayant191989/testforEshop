using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class EmailViewModel
    {
        public int Id { get; set; }
        public string To { get; set; }      
        public string Subject { get; set; }      
        public string Body { get; set; }
    }
}