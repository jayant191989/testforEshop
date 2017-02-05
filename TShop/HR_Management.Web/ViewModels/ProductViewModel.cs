using HR_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class AttributesTag
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public  IEnumerable<ProductAttributeOptions> ProductAttributeOptions { get; set; }
        public IEnumerable<OptionsTag> OptionsTags { get; set; }

        public bool IsChecked { get; set; }
    }

    public class OptionsTag
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }      
        public bool IsChecked { get; set; }
    }

    public class CategoryTag
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

    }

    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Code { get; set; }
        public string ModelNumber { get; set; }
        public string AutoGenerateName { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? DiscountPerUnitInPercent { get; set; }
        public string Discription { get; set; }
        public string Specifications { get; set; }
        public HttpPostedFileBase MainImageNameFile { get; set; }
        public string ThumbMainImageName { get; set; }
        public string ThumbMainImagePath { get; set; }
        public string ImagePath { get; set; }
        public decimal? ImageSize { get; set; }
        public string ImageExtention { get; set; }
        public int TabIndex { get; set; }
        public bool IsAvailableForSale { get; set; }
        public bool ShowOnWebsite { get; set; }
        public bool Is_NewArrival { get; set; }
        public bool Is_FeaturedProduct { get; set; }
        public bool Is_BestSeller { get; set; }
        public IList<ProductsAttributeViewModel> ProductsAttributeViewModels { get; set; }
        public IList<ProductAttributeOptionViewModel> ProductAttributeOptionViewModels { get; set; }
        public IList<CategoryTag> CategoryTags { get; set; }
        public IList<AttributesTag> AttributesTags { get; set; }
        public IList<OptionsTag> OptionsTags { get; set; }

    }
}