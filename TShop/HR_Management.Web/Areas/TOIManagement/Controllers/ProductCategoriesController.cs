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
using System.Threading.Tasks;
using TreeUtility;
using HR_Management.Web.ViewModels;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        private List<ProductCategory> GetListOfNodes()
        {
            List<ProductCategory> sourceCategories = _dbContext.ProductCategories.ToList();
            List<ProductCategory> categories = new List<ProductCategory>();
            foreach (ProductCategory sourceCategory in sourceCategories)
            {
                ProductCategory c = new ProductCategory();
                c.Id = sourceCategory.Id;
                c.Name = sourceCategory.Name;
                if (sourceCategory.ParentCategoryId != null)
                {
                    c.Parent = new ProductCategory();
                    c.Parent.Id = (Guid)sourceCategory.ParentCategoryId;
                }
                categories.Add(c);
            }
            return categories;
        }

        private string EnumerateNodes(ProductCategory parent)
        {
            // Init an empty string
            string content = String.Empty;

            // Add <li> category name
            content += "<li class=\"treenode\">";
            content += parent.Name;
            content += String.Format("<a href=\"/TOIManagement/ProductCategories/Edit/{0}\" class=\"btn btn-primary btn-xs treenodeeditbutton\">Edit</a>", parent.Id);
            content += String.Format("<a href=\"/TOIManagement/ProductCategories/Delete/{0}\" class=\"btn btn-danger btn-xs treenodedeletebutton\">Delete</a>", parent.Id);

            // If there are no children, end the </li>
            if (parent.Children.Count == 0)
                content += "</li>";
            else   // If there are children, start a <ul>
                content += "<ul>";

            // Loop one past the number of children
            int numberOfChildren = parent.Children.Count;
            for (int i = 0; i <= numberOfChildren; i++)
            {
                // If this iteration's index points to a child,
                // call this function recursively
                if (numberOfChildren > 0 && i < numberOfChildren)
                {
                    ProductCategory child = parent.Children[i];
                    content += EnumerateNodes(child);
                }

                // If this iteration's index points past the children, end the </ul>
                if (numberOfChildren > 0 && i == numberOfChildren)
                    content += "</ul>";
            }

            // Return the content
            return content;
        }

        public ActionResult Index()
        {
            // Start the outermost list
            string fullString = "<ul>";

            IList<ProductCategory> listOfNodes = GetListOfNodes();
            IList<ProductCategory> topLevelCategories = TreeHelper.ConvertToForest(listOfNodes);

            foreach (var category in topLevelCategories)
                fullString += EnumerateNodes(category);

            // End the outermost list
            fullString += "</ul>";

            return View((object)fullString);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = _dbContext.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        private void ValidateParentsAreParentless(ProductCategoryViewModel category)
        {
            // There is no parent
            if (category.ParentCategoryId == null)
                return;

            // The parent has a parent
            ProductCategory parentCategory = _dbContext.ProductCategories.Find(category.ParentCategoryId);
            if (parentCategory.ParentCategoryId != null)
                throw new InvalidOperationException("You cannot nest this category more than two levels deep.");

            // The parent does NOT have a parent, but the category being nested has children
            int numberOfChildren = _dbContext.ProductCategories.Count(c => c.ParentCategoryId == category.Id);
            if (numberOfChildren > 0)
                throw new InvalidOperationException("You cannot nest this category's children more than two levels deep.");
        }

        private SelectList PopulateParentCategorySelectList(Guid? id)
        {
            SelectList selectList;
            if (id == null)
                selectList = new SelectList(_dbContext.ProductCategories
                    .Where(c => c.ParentCategoryId == null), "Id", "Name");
            else if (_dbContext.ProductCategories.Count(c => c.ParentCategoryId == id) == 0)
                selectList = new SelectList(_dbContext.ProductCategories
                    .Where(c => c.ParentCategoryId == null && c.Id != id), "Id", "Name");
            else selectList = new SelectList(_dbContext.ProductCategories.Where(c => false), "Id", "CategoryName");
            return selectList;
        }

        public ActionResult Create()
        {
            ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel();
            ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(null);
            var productAttributes = _dbContext.ProductsAttributes;

            productCategoryViewModel.CategoryAttributesTags = productAttributes.Select(tag => new CategoryAttributesTag
            {
                Id = tag.Id,
                Name = tag.Name,
                IsChecked = false
            }).ToList();
            return View(productCategoryViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategoryViewModel viewModel, string[] AttributeOptions)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ValidateParentsAreParentless(viewModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(null);
                    return View(viewModel);
                }
                ProductCategory category = new ProductCategory();
                category.Id = Guid.NewGuid();
                category.ParentCategoryId = viewModel.ParentCategoryId;
                category.Name = viewModel.Name;
                _dbContext.ProductCategories.Add(category);

                if(viewModel.AttributeName!=null)
                {
                    ProductsAttribute productAttribute = new ProductsAttribute();
                    Guid attributeId = Guid.NewGuid();
                    productAttribute.Id = attributeId;
                    productAttribute.Name = viewModel.AttributeName;
                    _dbContext.ProductsAttributes.Add(productAttribute);

                    var attributeOptionsList = _dbContext.ProductAttributeOptions;
                    if (AttributeOptions != null)
                    {
                        foreach (var option in AttributeOptions)
                        {
                            ProductAttributeOptions attributeOption = new ProductAttributeOptions();
                            attributeOption.Id = Guid.NewGuid();
                            attributeOption.ProductsAttributesId = attributeId;
                            attributeOption.Name = option;                         
                            _dbContext.ProductAttributeOptions.Add(attributeOption);
                        }
                    }
                   // category.ProductsAttributes.Add(productAttribute);
                }
               
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(null);
            return View(viewModel);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = _dbContext.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }

            ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel();
            productCategoryViewModel.Id = productCategory.Id;
            productCategoryViewModel.Name = productCategory.Name;
            productCategoryViewModel.ParentCategoryId = productCategory.ParentCategoryId;

            var productAttributes = productCategory.ProductsAttributes.ToList();
            productCategoryViewModel.CategoryAttributesTags = productAttributes.Select(pa => new CategoryAttributesTag
            {
                Id = pa.Id,
                Name = pa.Name,
                CategoryOptionsTags = pa.ProductAttributeOptions.Select(x => new CategoryOptionsTag
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsChecked = false,
                }),
                IsChecked = productCategory.ProductsAttributes.Contains(pa),
            }).ToList();
            productCategoryViewModel.ProductslList = productCategory.Products.ToList();
            ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(productCategoryViewModel.Id);
            productCategoryViewModel.TabIndex = 1;
            return View(productCategoryViewModel);
        }

        public ActionResult EditWithTabIndex(Guid? id , int tabindex)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = _dbContext.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }

            ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel();
            productCategoryViewModel.Id = productCategory.Id;
            productCategoryViewModel.Name = productCategory.Name;
            productCategoryViewModel.ParentCategoryId = productCategory.ParentCategoryId;

            var productAttributes = productCategory.ProductsAttributes.ToList();
            productCategoryViewModel.CategoryAttributesTags = productAttributes.Select(pa => new CategoryAttributesTag
            {
                Id = pa.Id,
                Name = pa.Name,
                CategoryOptionsTags = pa.ProductAttributeOptions.Select(x => new CategoryOptionsTag
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsChecked = false,
                }),
                IsChecked = productCategory.ProductsAttributes.Contains(pa),
            }).ToList();
            productCategoryViewModel.ProductslList = productCategory.Products.ToList();
            ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(productCategoryViewModel.Id);           
            return View("Edit", productCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategoryViewModel productCategoryViewModel, string[] AttributeOptions)
        {
            if (ModelState.IsValid)
            {
                ProductCategory productCategory = _dbContext.ProductCategories.Find(productCategoryViewModel.Id);
                productCategory.Name = productCategoryViewModel.Name;
                productCategory.ParentCategoryId = productCategoryViewModel.ParentCategoryId;
                _dbContext.Entry(productCategory).State = EntityState.Modified;

                if (!string.IsNullOrEmpty(productCategoryViewModel.AttributeName))
                {
                    ProductsAttribute productAttribute = new ProductsAttribute();
                    Guid attributeId = Guid.NewGuid();
                    productAttribute.Id = attributeId;
                    productAttribute.Name = productCategoryViewModel.AttributeName;
                    _dbContext.ProductsAttributes.Add(productAttribute);
                    productCategory.ProductsAttributes.Add(productAttribute);

                    if (AttributeOptions != null)
                    {
                        foreach (var option in AttributeOptions)
                        {
                            if(option=="")
                            {

                            }
                            else
                            {
                                ProductAttributeOptions attributeOption = new ProductAttributeOptions();
                                attributeOption.Id = Guid.NewGuid();
                                attributeOption.ProductsAttributesId = attributeId;
                                attributeOption.Name = option;
                                //  ProductAttributeOptions retriveAttributeOption = attributeOptionsList.Where(t => t.Name == tag).FirstOrDefault();
                                // newBlog.Tags.Add(retriveTag);
                                _dbContext.ProductAttributeOptions.Add(attributeOption);
                            }
                           
                        }
                    }

                }
                //foreach (var option in productsAttribute.ProductAttributeOptions.ToArray())
                //{
                //    productsAttribute.ProductAttributeOptions.Remove(option);
                //}
                _dbContext.SaveChanges();
                ViewBag.ParentCategoryIdSelectList = PopulateParentCategorySelectList(productCategoryViewModel.Id);
                TempData["MessageToClientSuccess"] = "SuccessFully Saved";
                return RedirectToAction("Edit",new { id= productCategoryViewModel.Id });
            }
            return View(productCategoryViewModel);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = _dbContext.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductCategory productCategory = _dbContext.ProductCategories.Find(id);
            _dbContext.ProductCategories.Remove(productCategory);
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
    }
}
