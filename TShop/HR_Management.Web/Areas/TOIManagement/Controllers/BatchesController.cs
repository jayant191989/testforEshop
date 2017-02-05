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
    public class BatchesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetBatchesForDatatable()
        {
            var batches = _dbContext.Batch.ToList();
            List<BatchViewModel> batchViewModelList = new List<BatchViewModel>();
            foreach (var batch in batches)
            {
                BatchViewModel batchViewModel = new BatchViewModel();
                batchViewModel.Id = batch.Id;
                batchViewModel.Name = batch.Name;
                batchViewModel.EnteredDate = batch.EnteredDate;
                batchViewModelList.Add(batchViewModel);
                List<StoreProductsViewModel> storeProductsViewModelList = new List<StoreProductsViewModel>();
                foreach (var storeProduct in batch.StoreProducts)
                {
                    StoreProductsViewModel storeProductsViewModel = new StoreProductsViewModel();
                    storeProductsViewModel.Id = storeProduct.Id;
                    storeProductsViewModel.ProductName = storeProduct.Product.Name;
                    storeProductsViewModel.Quantity = storeProduct.Quantity;
                    storeProductsViewModelList.Add(storeProductsViewModel);                  
                }             
                batchViewModel.StoreProductsViewModel = storeProductsViewModelList;
            }
           
            //     var jsonObject = JsonConvert.SerializeObject(ProductViewModelList);
            // return View();
            var result = new
            {
                iTotalRecords = batches.Count,
                iTotalDisplayRecords = batches.Count,
                aaData = batchViewModelList
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = _dbContext.Batch.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }
        public ActionResult _Create()
        {
            ViewBag.StoreId = new SelectList(_dbContext.Stores.ToList(), "Id", "Name");
            return PartialView("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(Batch batch)
        {
            if (ModelState.IsValid)
            {
                batch.Id = Guid.NewGuid();
                _dbContext.Batch.Add(batch);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return View(batch);
        }

        public ActionResult _Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = _dbContext.Batch.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreId = new SelectList(_dbContext.Stores.ToList(), "Id", "Name",batch.StoreId);
            return PartialView("_Edit", batch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit(Batch batch)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(batch).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return View(batch);
        }

        public ActionResult Create()
        {
            ViewBag.StoreId = new SelectList(_dbContext.Stores.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Batch batch)
        {
            if (ModelState.IsValid)
            {
                batch.Id = Guid.NewGuid();
                _dbContext.Batch.Add(batch);
                _dbContext.SaveChanges();
                TempData["MessageToClientSuccess"] = "SuccessFully Saved";
                return RedirectToAction("Index");
            }
            return View(batch);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = _dbContext.Batch.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreId = new SelectList(_dbContext.Stores.ToList(), "Id", "Name", batch.StoreId);
            return View(batch);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = _dbContext.Batch.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        public ActionResult _Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = _dbContext.Batch.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", batch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Batch batch = _dbContext.Batch.Find(id);
            _dbContext.Batch.Remove(batch);
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
