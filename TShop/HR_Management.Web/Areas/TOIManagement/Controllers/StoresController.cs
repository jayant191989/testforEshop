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

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class StoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult IndexFullHeight()
        {
            StoreIndexViewModel storeIndexViewModel = new StoreIndexViewModel();
            List<StoreViewModel> storeViewModelLst = new List<StoreViewModel>();
            var stores = db.Stores.ToList();
            var products = db.Products.ToList();
            foreach (var store in stores)
            {
                StoreViewModel storeViewModel = new StoreViewModel();
                storeViewModel.Id = store.Id;
                storeViewModel.Name = store.Name;
                storeViewModelLst.Add(storeViewModel);
                List<BatchViewModel> batchesViewModelLst = new List<BatchViewModel>();
                foreach (var batch in store.Batches)
                {
                    BatchViewModel batchViewModel = new BatchViewModel();
                    batchViewModel.Id = batch.Id;
                    batchViewModel.StoreId = batch.StoreId;
                    batchViewModel.Name = batch.Name;
                    batchViewModel.EnteredDate = batch.EnteredDate;
                    batchesViewModelLst.Add(batchViewModel);
                    List<StoreProductsViewModel> storeProductsViewModelLst = new List<StoreProductsViewModel>();
                    if(batch.StoreProducts.Count!=0)
                    {
                        foreach (var storeProduct in batch.StoreProducts)
                        {
                            StoreProductsViewModel storeProductsViewModel = new StoreProductsViewModel();
                            storeProductsViewModel.Id = storeProduct.Id;
                            storeProductsViewModel.BatchId = storeProduct.BatchId;
                            storeProductsViewModel.ProductId = storeProduct.ProductId;
                            storeProductsViewModel.ProductEnterDate = storeProduct.ProductEnterDate;
                            Product product = products.Where(p => p.Id == storeProduct.ProductId).FirstOrDefault();
                            storeProductsViewModel.ProductName = product.Name;
                            storeProductsViewModel.AutoGenerateName = product.AutoGenerateName;
                            storeProductsViewModel.Code = product.Code;
                            storeProductsViewModel.ModelNumber = product.ModelNumber;
                            storeProductsViewModel.MRPPerUnit = storeProduct.MRPPerUnit;
                            storeProductsViewModel.CostPricePerUnit = storeProduct.CostPricePerUnit;
                            storeProductsViewModel.DiscountRatePerUnit = storeProduct.DiscountRatePerUnit;
                            storeProductsViewModel.Quantity = storeProduct.Quantity;
                            storeProductsViewModelLst.Add(storeProductsViewModel);
                            //batchViewModel.StoreProductsViewModel.Add(storeProductsViewModel);
                        }
                    }                    
                    batchViewModel.StoreProductsViewModel = storeProductsViewModelLst;
                }
                storeViewModel.BatchesViewModel = batchesViewModelLst;
            }
            storeIndexViewModel.StoreViewModel = storeViewModelLst;
            return View(storeIndexViewModel);
        }
        public ActionResult Index()
        {            
            var store = db.Stores.ToList();          
            return View(store);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Store store)
        {
            if (ModelState.IsValid)
            {
                store.Id = Guid.NewGuid();
                db.Stores.Add(store);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(store);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(store);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Store store = db.Stores.Find(id);
            db.Stores.Remove(store);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
