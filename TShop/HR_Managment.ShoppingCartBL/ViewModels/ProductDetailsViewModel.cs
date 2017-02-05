using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.ShoppingCartBL.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string ThumbMainImageName { get; set; }       
      
        public string Code { get; set; }
        public string ModelNumber { get; set; }
        public string AutoGenerateName { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string Specifications { get; set; }
        public decimal? CostPricePerUnit { get; set; }
        public decimal? Discount { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? Quantity { get; set; }
        public IEnumerable<ProductImagesViewModel> ProductImagesViewModelList { get; set; }
    }
}
