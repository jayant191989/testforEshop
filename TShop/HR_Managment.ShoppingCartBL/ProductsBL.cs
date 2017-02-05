using HR_Management.Context;
using HR_Management.Models;
using HR_Management.ShoppingCartBL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.ShoppingCartBL
{
    public class ProductsBL
    {
        public List<ProductViewModel> GetProductList()
        {
            List<ProductViewModel> prdList = new List<ProductViewModel>();
            using (var db = new ApplicationDbContext())
            {
                var productList = db.Products.Distinct().ToList();
                foreach (var p in productList)
                {
                    ProductViewModel viewModel = new ProductViewModel();
                    viewModel.Id = p.Id;
                    viewModel.AutoGenerateName = p.AutoGenerateName;
                    viewModel.Discription = p.Discription;
                    viewModel.Title = p.Title;
                    viewModel.ThumbMainImageName = p.ThumbMainImageName;
                    viewModel.CategoryName = p.ProductCategory.Name;
                    // ThumbMainImagePath = "Images/" + p.Product.ProductCategory.Name + "/" + p.Product.ThumbMainImageName,
                    //  viewModel.MRPPerUnit = p.m,                    
                    viewModel.Discount = p.DiscountPerUnitInPercent;
                    viewModel.SalePrice = p.SalePrice;
                    prdList.Add(viewModel);
                }
            }
            return prdList;
        }

        public ProductDetailsViewModel GetProductDetail(Guid? productId)
        {
            ProductDetailsViewModel viewModel = new ProductDetailsViewModel();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                List<ProductImagesViewModel> productImagesViewModelList = new List<ProductImagesViewModel>();
                Product product = db.Products.Where(p => p.Id == productId).FirstOrDefault();
                viewModel.Id = product.Id;
                viewModel.Discription = product.Discription;
                viewModel.Specifications = product.Specifications;
                viewModel.Code = product.Code;
                viewModel.ModelNumber = product.ModelNumber;
                viewModel.Title = product.Title;
                viewModel.ThumbMainImageName = product.ThumbMainImageName;
                viewModel.Discount = product.DiscountPerUnitInPercent;
                viewModel.SalePrice = product.SalePrice;
                // ThumbMainImagePath = "Images/" + p.Product.ProductCategory.Name + "/" + p.Product.ThumbMainImageName,
                //  viewModel.MRPPerUnit = p.m,
                foreach(var image in product.ProductImages.ToList())
                {
                    ProductImagesViewModel productImagesViewModel = new ProductImagesViewModel();
                    productImagesViewModel.Id = image.Id;
                    productImagesViewModel.FullName = image.FullName;
                    productImagesViewModel.Extention = image.Extention;
                    productImagesViewModelList.Add(productImagesViewModel);
                }
                viewModel.ProductImagesViewModelList = productImagesViewModelList;
            }
            return viewModel;
        }
    }
}