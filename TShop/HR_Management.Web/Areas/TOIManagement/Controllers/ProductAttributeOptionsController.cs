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

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class ProductAttributeOptionsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var productAttributeOptions = _dbContext.ProductAttributeOptions.Include(p => p.ProductsAttribute);
            return View(productAttributeOptions.ToList());
        }
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributeOptions productAttributeOptions = _dbContext.ProductAttributeOptions.Find(id);
            if (productAttributeOptions == null)
            {
                return HttpNotFound();
            }
            return View(productAttributeOptions);
        }

        public ActionResult Create(Guid productsAttributesId)
        {
            ProductAttributeOptions option = new ProductAttributeOptions();
            option.ProductsAttributesId = productsAttributesId;
            return PartialView("_Create", option);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductAttributeOptions productAttributeOptions)
        {
            if (ModelState.IsValid)
            {
                productAttributeOptions.Id = Guid.NewGuid();
                _dbContext.ProductAttributeOptions.Add(productAttributeOptions);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return View(productAttributeOptions);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributeOptions productAttributeOptions = _dbContext.ProductAttributeOptions.Find(id);
            if (productAttributeOptions == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Edit", productAttributeOptions);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductAttributeOptions productAttributeOptions)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(productAttributeOptions).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return View(productAttributeOptions);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributeOptions productAttributeOptions = _dbContext.ProductAttributeOptions.Find(id);
            if (productAttributeOptions == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", productAttributeOptions);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductAttributeOptions productAttributeOptions = _dbContext.ProductAttributeOptions.Find(id);
            _dbContext.ProductAttributeOptions.Remove(productAttributeOptions);
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
