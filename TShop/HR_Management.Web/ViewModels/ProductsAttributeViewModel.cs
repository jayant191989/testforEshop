using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class AttributesOptionsTag
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
       
    }   

    public class ProductsAttributeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OptionValue { get; set; }
        public IList<AttributesOptionsTag> AttributesOptionsTags { get; set; }
    }
}