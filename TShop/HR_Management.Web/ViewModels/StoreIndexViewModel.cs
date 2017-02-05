using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Management.Web.ViewModels
{
    public class StoreIndexViewModel
    {
        public IEnumerable<StoreViewModel> StoreViewModel { get; set; }        
        
    }

    public class StoreViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BatchViewModel> BatchesViewModel { get; set; }
    }

    public class BatchViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime EnteredDate { get; set; }
        public Guid StoreId { get; set; }
        public virtual StoreViewModel StoreViewModel { get; set; }
        public virtual ICollection<StoreProductsViewModel> StoreProductsViewModel { get; set; }
    }

    public class StoreProductsViewModel
    {
        public Guid Id { get; set; }
        public string BatchNumber { get; set; }
      
        public Guid StoreId { get; set; }
        [Required]
        public Guid BatchId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string Code { get; set; }
        public string ModelNumber { get; set; }
        public string AutoGenerateName { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string Specifications { get; set; }        
        public DateTime ProductEnterDate { get; set; }
        public decimal? CostPricePerUnit { get; set; }
        public decimal? DiscountRatePerUnit { get; set; }
        public decimal? MRPPerUnit { get; set; }
        public decimal SalePrice { get; set; }    
        public bool IsAvailableForSale { get; set; }
        public bool ShowOnWebsite { get; set; }
        [Required]
        public decimal? Quantity { get; set; }     
        public virtual BatchViewModel BatchViewModel { get; set; }
    }
}