using HR_Management.ShoppingCartBL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class LayoutViewModel
    {
        public bool IsLoggedIn { get; set; }
        public string Email { get; set; }      
    }

    public class LayOutTopbar
    {
        public int CmsSectionLanguageId { get; set; }
        public string MenuName { get; set; }
        public int ModuleId { get; set; }
    }

    public class TopNavbar
    {
        public int CmsSectionLanguageId { get; set; }
        public string MenuName { get; set; }
        public int ModuleId { get; set; }
        public IEnumerable<CartViewModel> CartViewModelList { get; set; }
    }
}