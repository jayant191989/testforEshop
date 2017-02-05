using HR_Management.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeUtility;

namespace HR_Management.Models
{
    public class ProductCategory : ITreeNode<ProductCategory>
    {

        public Guid Id { get; set; }

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

        public string Name { get; set; }
        public virtual ProductCategory Parent { get; set; }
        public IList<ProductCategory> Children { get; set; }
        public virtual IList<Product> Products { get; set; }
        public virtual IList<ProductsAttribute> ProductsAttributes { get; set; }
    }
}
