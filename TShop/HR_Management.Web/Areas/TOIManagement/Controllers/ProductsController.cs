using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Management.Context;
using HR_Management.Models;
using HR_Management.Web.ViewModels;
using System.IO;
using Newtonsoft.Json;
using ImageResizer;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult CategoryProductsIndex(Guid categoryId)
        {
            return View(_dbContext.Products.Where(p => p.ProductCategoryId == categoryId).ToList());
        }

        public ActionResult ProductsByCategory(Guid productCategoryId)
        {
            var products = _dbContext.Products.Where(p => p.ProductCategoryId == productCategoryId).ToList();
            ViewBag.ProductCategoryId = productCategoryId;
            return PartialView("_ProductsByCategory", products);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProductForDatatable()
        {
            var products = _dbContext.Products.ToList();
            List<ProductViewModel> ProductViewModelList = new List<ProductViewModel>();
            foreach (var product in products)
            {
                ProductViewModel productViewModel = new ProductViewModel();
                productViewModel.Id = product.Id;
                productViewModel.Name = product.Name;
                productViewModel.SalePrice = product.SalePrice;
                productViewModel.Code = product.Code;
                productViewModel.AutoGenerateName = product.AutoGenerateName;
                ProductViewModelList.Add(productViewModel);
                List<ProductsAttributeViewModel> productsAttributeViewModelList = new List<ProductsAttributeViewModel>();
                foreach (var attribute in product.ProductsAttributes)
                {
                    ProductsAttributeViewModel productsAttributeViewModel = new ProductsAttributeViewModel();
                    productsAttributeViewModel.Id = attribute.Id;
                    productsAttributeViewModel.Name = attribute.Name;
                    //  List<ProductAttributeOptionViewModel> productAttributeOptionViewModelList = new List<ProductAttributeOptionViewModel>();
                    foreach (var option in attribute.ProductAttributeOptions)
                    {
                        if (option.Products.Contains(product))
                        {
                            productsAttributeViewModel.OptionValue = option.Name;
                            //ProductAttributeOptionViewModel productAttributeOptionViewModel = new ProductAttributeOptionViewModel();
                            //productAttributeOptionViewModel.Id = option.Id;
                            //productAttributeOptionViewModel.Name = option.Name;
                            //productAttributeOptionViewModelList.Add(productAttributeOptionViewModel);
                        }
                    }
                    productsAttributeViewModelList.Add(productsAttributeViewModel);
                    //  productViewModel.ProductAttributeOptionViewModels = productAttributeOptionViewModelList;
                }

                productViewModel.ProductsAttributeViewModels = productsAttributeViewModelList;
            }
            //     var jsonObject = JsonConvert.SerializeObject(ProductViewModelList);
            // return View();
            var result = new
            {
                iTotalRecords = products.Count,
                iTotalDisplayRecords = products.Count,
                aaData = ProductViewModelList
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        public ActionResult SaveUploadedFile(HttpPostedFileBase[] images, Guid productId)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    Product product = _dbContext.Products.Find(productId);

                    product.ImageSize = file.ContentLength;
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");
                        var fileName1 = Path.GetFileName(file.FileName);

                        string ImageNameWithOutExtention = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        product.Name = ImageNameWithOutExtention;
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);
                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        product.ImagePath = path;
                        product.ImageExtention = extension;
                        file.SaveAs(path);
                        _dbContext.Products.Add(product);
                        _dbContext.SaveChanges();
                    }
                }
                return RedirectToAction("CreatePost", new { images });
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        public ActionResult _GetAttributeAndOptionsByCategory(Guid productCategoryId)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            var productAttributes = _dbContext.ProductsAttributes;
            ProductCategory productCategory = _dbContext.ProductCategories.Where(c => c.Id == productCategoryId).FirstOrDefault();
            productViewModel.ProductCategoryId = productCategoryId;
            productViewModel.AttributesTags = productCategory.ProductsAttributes.Select(tag => new AttributesTag
            {
                Id = tag.Id,
                Name = tag.Name,
                ProductAttributeOptions = tag.ProductAttributeOptions,
                IsChecked = false
            }).ToList();
            string html = "~/Areas/TOIManagement/Views/Products/_ProductAttributeAndOption.cshtml";
            string modelString = RenderRazorViewToString(html, productViewModel);
            return Json(new { ModelString = modelString });
        }
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext =
                     new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        public ActionResult Create()
        {
            var products = _dbContext.ProductCategories.ToList();
            ViewBag.ProductCategoryId = new SelectList(products, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ProductViewModel viewModel, Guid[] AttributeSelectedOnView, Guid[] OptionsSelectedOnView)
        {
            // string data = Convert.ToString(HttpContext.Request.Params["Name"]);
            var productAttList = _dbContext.ProductsAttributes;
            var productCategory = _dbContext.ProductCategories.Find(viewModel.ProductCategoryId);
            if (ModelState.IsValid)
            {
                Product product = new Product();
                Guid productid = Guid.NewGuid();
                product.Id = productid;
                product.Code = viewModel.Code;
                product.ProductCategoryId = viewModel.ProductCategoryId;
                product.ModelNumber = viewModel.ModelNumber;
                product.Name = viewModel.Name;
                product.Title = viewModel.Title;
                product.DiscountPerUnitInPercent = viewModel.DiscountPerUnitInPercent;
                product.SalePrice = viewModel.SalePrice;
                product.IsAvailableForSale = viewModel.IsAvailableForSale;
                product.Is_BestSeller = viewModel.Is_BestSeller;
                product.Is_NewArrival = viewModel.Is_NewArrival;
                product.ShowOnWebsite = viewModel.ShowOnWebsite;
                product.Is_FeaturedProduct = viewModel.Is_FeaturedProduct;
                if (viewModel.ModelNumber != null)
                {
                    product.AutoGenerateName = viewModel.Code + "/" + viewModel.Name + "/" + viewModel.ModelNumber + " ";
                }
                else
                {
                    product.AutoGenerateName = viewModel.Code + "/" + viewModel.Name + " ";
                }

                product.Discription = viewModel.Discription;
                product.Specifications = viewModel.Specifications;
                if (viewModel.MainImageNameFile != null)
                {
                    string fName = "";
                    HttpPostedFileBase file = viewModel.MainImageNameFile;
                    fName = viewModel.MainImageNameFile.FileName;
                    string ImageNameWithOutExtention = System.IO.Path.GetFileNameWithoutExtension(fName);
                    string extension = Path.GetExtension(fName);

                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Upload", Server.MapPath(@"\")));
                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), productCategory.Name);
                    var fileName1 = Path.GetFileName(file.FileName);
                    bool isExists = System.IO.Directory.Exists(pathString);
                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);
                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);


                    var versions = new Dictionary<string, string>();
                    var imagePath = string.Format("{0}\\{1}", pathString, ImageNameWithOutExtention);

                    versions.Add("_small", "maxwidth=100&maxheight=100&format=jpg");
                    versions.Add("_medium", "maxwidth=500&maxheight=500&format=jpg");
                    versions.Add("_large", "maxwidth=900&maxheight=900&format=jpg");
                    foreach (var suffix in versions.Keys)
                    {
                        file.InputStream.Seek(0, SeekOrigin.Begin);
                        ImageBuilder.Current.Build(
                            new ImageJob(
                                file.InputStream,
                               imagePath + suffix,
                                new Instructions(versions[suffix]),
                                false,
                                true));
                    }

                    product.ThumbMainImageName = ImageNameWithOutExtention;
                    product.ImageExtention = extension;
                }


                SaveProduct(product);
                Product retriveProduct = _dbContext.Products.Find(productid);

                //var productCategory = _dbContext.ProductCategories;
                //if (CategorySelectedOnView != null)
                //{
                //    foreach (var att in CategorySelectedOnView)
                //    {
                //        ProductCategory category = productCategory.Where(pa => pa.Id == att).FirstOrDefault();
                //        // attr.Products.Add(product);
                //        category.Products.Add(retriveProduct);
                //    }
                //}
                string autoName = string.Empty;
                autoName += "(";
                if (OptionsSelectedOnView != null)
                {
                    foreach (var option in OptionsSelectedOnView)
                    {
                        ProductAttributeOptions productAttrOption = _dbContext.ProductAttributeOptions.Where(po => po.Id == option).FirstOrDefault();
                        autoName += productAttrOption.ProductsAttribute.Name + "-" + productAttrOption.Name + "/";
                        productAttrOption.ProductsAttribute.Products.Add(retriveProduct);
                        productAttrOption.Products.Add(retriveProduct);
                    }
                }
                autoName += ")";
                retriveProduct.AutoGenerateName = autoName;
                ModifyProduct(retriveProduct);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        private void SaveProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        private void ModifyProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Id = product.Id;
            productViewModel.ProductCategoryId = product.ProductCategoryId;
            productViewModel.Name = product.Name;
            productViewModel.Code = product.Code;
            productViewModel.ModelNumber = product.ModelNumber;
            productViewModel.Title = product.Title;
            productViewModel.Discription = product.Discription;
            productViewModel.Specifications = product.Specifications;
            productViewModel.ThumbMainImageName = product.ThumbMainImageName;
            productViewModel.CategoryName = product.ProductCategory.Name;
            productViewModel.DiscountPerUnitInPercent = product.DiscountPerUnitInPercent;
            productViewModel.SalePrice = product.SalePrice;
            productViewModel.IsAvailableForSale = product.IsAvailableForSale;
            productViewModel.Is_BestSeller = product.Is_BestSeller;
            productViewModel.Is_NewArrival = product.Is_NewArrival;
            productViewModel.ShowOnWebsite = product.ShowOnWebsite;
            productViewModel.Is_FeaturedProduct = product.Is_FeaturedProduct;
            //  var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Upload", Server.MapPath(@"\")));
            //    productViewModel.ThumbMainImagePath = originalDirectory + "\\" + product.ProductCategory.Name + "\\" + product.ThumbMainImageName + product.ImageExtention;
            var productAttributes = product.ProductCategory.ProductsAttributes.ToList();
            var productCategory = _dbContext.ProductCategories.ToList();
            productViewModel.MainImageNameFile = null;
            productViewModel.AttributesTags = productAttributes.Select(pa => new AttributesTag
            {
                Id = pa.Id,
                Name = pa.Name,
                OptionsTags = pa.ProductAttributeOptions.Select(x => new OptionsTag
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsChecked = product.ProductAttributeOptions.Contains(x),
                }),
                IsChecked = product.ProductsAttributes.Contains(pa),
            }).ToList();

            //productViewModel.CategoryTags = productCategory.Select(pc => new CategoryTag
            //{
            //    Id = pc.Id,
            //    Name = pc.Name,
            //    IsChecked = product.ProductCategories.Contains(pc),
            //}).ToList();

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ProductViewModel viewModel, string[] AttributeSelectedOnView, Guid[] OptionsSelectedOnView, string[] CategorySelectedOnView)
        {
            if (ModelState.IsValid)
            {
                Product product = _dbContext.Products.Find(viewModel.Id);
                product.Name = viewModel.Name;
                product.Code = viewModel.Code;
                product.ModelNumber = viewModel.ModelNumber;
                product.Title = viewModel.Title;
                product.Discription = viewModel.Discription;
                product.Specifications = viewModel.Specifications;
                product.DiscountPerUnitInPercent = viewModel.DiscountPerUnitInPercent;
                product.SalePrice = viewModel.SalePrice;
                product.IsAvailableForSale = viewModel.IsAvailableForSale;
                product.Is_BestSeller = viewModel.Is_BestSeller;
                product.Is_NewArrival = viewModel.Is_NewArrival;
                product.ShowOnWebsite = viewModel.ShowOnWebsite;
                product.Is_FeaturedProduct = viewModel.Is_FeaturedProduct;

                if (viewModel.ModelNumber != null)
                {
                    product.AutoGenerateName = viewModel.Code + "/" + viewModel.Name + "/" + viewModel.ModelNumber + " ";
                }
                else
                {
                    product.AutoGenerateName = viewModel.Code + "/" + viewModel.Name + " ";
                }
                if (viewModel.MainImageNameFile != null)
                {
                    string fName = "";
                    HttpPostedFileBase file = viewModel.MainImageNameFile;
                    fName = viewModel.MainImageNameFile.FileName;
                    string ImageNameWithOutExtention = System.IO.Path.GetFileNameWithoutExtension(fName);
                    string extension = Path.GetExtension(fName);

                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Upload", Server.MapPath(@"\")));
                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), product.ProductCategory.Name);
                    var fileName1 = Path.GetFileName(file.FileName);
                    bool isExists = System.IO.Directory.Exists(pathString);
                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);
                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);


                    var versions = new Dictionary<string, string>();
                    var imagePath = string.Format("{0}\\{1}", pathString, ImageNameWithOutExtention);

                    versions.Add("_small", "maxwidth=100&maxheight=100&format=jpg");
                    versions.Add("_medium", "maxwidth=500&maxheight=500&format=jpg");
                    versions.Add("_large", "maxwidth=900&maxheight=900&format=jpg");
                    foreach (var suffix in versions.Keys)
                    {
                        file.InputStream.Seek(0, SeekOrigin.Begin);
                        ImageBuilder.Current.Build(
                            new ImageJob(
                                file.InputStream,
                               imagePath + suffix,
                                new Instructions(versions[suffix]),
                                false,
                                true));
                    }
                    product.ThumbMainImageName = ImageNameWithOutExtention;
                    product.ImageExtention = extension;
                }
                // var productAttList = _dbContext.ProductsAttributes;
                //  var productCategory = _dbContext.ProductCategories;
                //if (CategorySelectedOnView != null)
                //{
                //    foreach (var attr in product.ProductCategories.ToArray())
                //    {
                //        product.ProductCategories.Remove(attr);
                //    }
                //    foreach (var att in CategorySelectedOnView)
                //    {
                //        ProductCategory category = productCategory.Where(pa => pa.Name == att).FirstOrDefault();
                //        // attr.Products.Add(product);
                //        product.ProductCategories.Add(category);
                //    }
                //}
                var productOptions = _dbContext.ProductAttributeOptions;
                string autoName = string.Empty;
                foreach (var option in product.ProductAttributeOptions)
                {
                    ProductAttributeOptions productAttrOption = productOptions.Where(po => po.Id == option.Id).FirstOrDefault();
                    // product.ProductAttributeOptions.Remove(option);
                    productAttrOption.Products.Remove(product);
                }
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.SaveChanges();
                autoName += "(";
                if (OptionsSelectedOnView != null)
                {
                    foreach (var option in OptionsSelectedOnView)
                    {
                        ProductAttributeOptions productAttrOption = productOptions.Where(po => po.Id == option).FirstOrDefault();
                        autoName += productAttrOption.ProductsAttribute.Name + "-" + productAttrOption.Name + "/";
                        product.ProductAttributeOptions.Add(productAttrOption);
                        //  productAttrOption.ProductsAttribute.Products.Add(product);
                    }
                }
                autoName += ")";
                product.AutoGenerateName = product.AutoGenerateName + autoName;
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
        public ActionResult GetAttachments()
        {
            //Get the images list from repository
            ProductImage[] attachmentsList = _dbContext.ProductImages.ToArray();
            return Json(new { Data = attachmentsList.Select(m => new { Id = m.Id, Path = string.Concat("file://" + m.Path), Name = m.ImageName, Size = m.Size }) }, JsonRequestBehavior.AllowGet);
            //  return Json(new { Data = attachmentsList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult GetProductsForAutocomplete(string term)
        {
            Product[] productsMatching = String.IsNullOrWhiteSpace(term) ? null
                : _dbContext.Products.Where(ii => ii.Code.Contains(term) || ii.Name.Contains(term)).ToArray();

            return Json(productsMatching.Select(m => new
            {
                Id = m.Id,
                value = m.Name,
                label = String.Format("{0}: {1}", m.AutoGenerateName, m.Code),
                Name = m.Name,
                Code = m.Code,
                AutoGenerateName = m.AutoGenerateName,
                ModelNumber = m.ModelNumber
            }), JsonRequestBehavior.AllowGet);
        }
    }
}
