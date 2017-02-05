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

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class ProductsAttributesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(_dbContext.ProductsAttributes.ToList());
        }


        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsAttribute productsAttribute = _dbContext.ProductsAttributes.Find(id);
            if (productsAttribute == null)
            {
                return HttpNotFound();
            }
            return View(productsAttribute);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductsAttributeViewModel viewModel, string[] AttributeOptions)
        {
            if (ModelState.IsValid)
            {
                ProductsAttribute productAttribute = new ProductsAttribute();
                Guid attributeId = Guid.NewGuid();
                productAttribute.Id = attributeId;
                productAttribute.Name = viewModel.Name;
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
                        //  ProductAttributeOptions retriveAttributeOption = attributeOptionsList.Where(t => t.Name == tag).FirstOrDefault();
                        // newBlog.Tags.Add(retriveTag);
                        _dbContext.ProductAttributeOptions.Add(attributeOption);
                    }
                }

                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsAttribute productsAttribute = _dbContext.ProductsAttributes.Find(id);
            if (productsAttribute == null)
            {
                return HttpNotFound();
            }
            ProductsAttributeViewModel productAttributeViewModel = new ProductsAttributeViewModel();
            productAttributeViewModel.Id = productsAttribute.Id;
            productAttributeViewModel.Name = productsAttribute.Name;
            if (productsAttribute.ProductAttributeOptions != null)
            {
                productAttributeViewModel.AttributesOptionsTags = productsAttribute.ProductAttributeOptions.Select(tag => new AttributesOptionsTag
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    IsChecked = false
                }).ToList();

            }
            return PartialView("_Edit", productAttributeViewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductsAttributeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ProductsAttribute productsAttribute = _dbContext.ProductsAttributes.Find(viewModel.Id);
                productsAttribute.Name = viewModel.Name;
                _dbContext.Entry(productsAttribute).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return View(viewModel);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsAttribute productsAttribute = _dbContext.ProductsAttributes.Find(id);
            if (productsAttribute == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", productsAttribute);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductsAttribute productsAttribute = _dbContext.ProductsAttributes.Find(id);
            _dbContext.ProductsAttributes.Remove(productsAttribute);
            _dbContext.SaveChanges();
            return Json(new { success = true });
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
