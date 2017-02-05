using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class CategoryAttributesTag
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public IEnumerable<CategoryOptionsTag> CategoryOptionsTags { get; set; }
    }
    public class CategoryOptionsTag
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
    public class ProductCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int  TabIndex { get; set; }
        public string AttributeName { get; set; }
        private Guid? _parentCategoryId;

        [Display(Name = "Parent Category")]
        public Guid? ParentCategoryId
        {
            get { return _parentCategoryId; }
            set
            {
                if (Id == value)
                    throw new InvalidOperationException("A category cannot have itself as its parent.");

                _parentCategoryId = value;
            }
        }
        public IList<CategoryAttributesTag> CategoryAttributesTags { get; set; }
        public IList<Product> ProductslList { get; set; }
    }
}