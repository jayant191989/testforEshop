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
    public class StoreProductsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.Batches = new SelectList(_dbContext.Batch, "Id", "Name");
            ViewBag.Stores = new SelectList(_dbContext.Stores, "Id", "Name");
            ViewBag.Contacts = new SelectList(_dbContext.Contacts, "Id", "FullName");

            var storeProducts = _dbContext.StoreProducts.OrderBy(d=>d.CreatedDate).ToList();
            List<StoreProductsViewModel> storeProductsViewModelList = new List<StoreProductsViewModel>();
            IEnumerable<StoreProductsViewModel> storeProductsViewModelEnu;
            foreach (var storeProduct in storeProducts)
            {
                StoreProductsViewModel viewModel = new StoreProductsViewModel();
                viewModel.Id = storeProduct.Id;
                viewModel.ProductName = storeProduct.Product.Name;
                viewModel.ProductEnterDate = storeProduct.ProductEnterDate;
                viewModel.Quantity = storeProduct.Quantity;
                viewModel.BatchNumber = storeProduct.Batch.Name;
                viewModel.AutoGenerateName = storeProduct.Product.AutoGenerateName;
                storeProductsViewModelList.Add(viewModel);
            }
            storeProductsViewModelEnu = storeProductsViewModelList;
            return View(storeProductsViewModelEnu);
        }

        public ActionResult _Index(Guid batchId)
        {
            ViewBag.BatchId = batchId;
            return PartialView("_Index");
        }

        public ActionResult GetStoreProductForDatatable(Guid? batchId)
        {
            // Guid guidBatchId = Guid.Parse(batchId);
            var storeProducts = _dbContext.StoreProducts.Where(s => s.BatchId == batchId).ToList();
            List<StoreProductsViewModel> storeProductsViewModelList = new List<StoreProductsViewModel>();
            foreach (var storeProduct in storeProducts)
            {
                StoreProductsViewModel storeProductViewModel = new StoreProductsViewModel();
                storeProductViewModel.Id = storeProduct.Id;
                storeProductViewModel.ProductEnterDate = storeProduct.ProductEnterDate;
                storeProductViewModel.MRPPerUnit = storeProduct.MRPPerUnit;
                storeProductViewModel.ProductName = storeProduct.Product.Name;
                storeProductViewModel.Quantity = storeProduct.Quantity;
                storeProductsViewModelList.Add(storeProductViewModel);
            }

            var result = new
            {
                iTotalRecords = storeProducts.Count,
                iTotalDisplayRecords = storeProducts.Count,
                aaData = storeProductsViewModelList
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreProduct storeProduct = _dbContext.StoreProducts.Find(id);
            if (storeProduct == null)
            {
                return HttpNotFound();
            }
            return View(storeProduct);
        }

        public ActionResult Create()
        {
            ViewBag.Batches = new SelectList(_dbContext.Batch, "Id", "Name");
            ViewBag.Stores = new SelectList(_dbContext.Stores, "Id", "Name");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreProductsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                StoreProduct storeProduct = new StoreProduct();
                storeProduct.Id = Guid.NewGuid();
                storeProduct.BatchId = viewModel.BatchId;
                storeProduct.CostPricePerUnit = viewModel.CostPricePerUnit;
                storeProduct.Quantity = viewModel.Quantity;
                storeProduct.CostPricePerUnit = viewModel.CostPricePerUnit;
                storeProduct.MRPPerUnit = viewModel.MRPPerUnit;
                storeProduct.ProductEnterDate = viewModel.ProductEnterDate;
                storeProduct.ProductId = viewModel.ProductId;
             //   storeProduct.SalePrice = viewModel.SalePrice;
                _dbContext.StoreProducts.Add(storeProduct);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Batches = new SelectList(_dbContext.Batch, "Id", "Name");
            ViewBag.Stores = new SelectList(_dbContext.Stores, "Id", "Name");
            return View(viewModel);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreProduct storeProduct = _dbContext.StoreProducts.Find(id);
            StoreProductsViewModel viewModel = new StoreProductsViewModel();
            viewModel.Id = storeProduct.Id;
            viewModel.BatchId = storeProduct.BatchId;
            viewModel.StoreId = storeProduct.Batch.StoreId;
            viewModel.BatchNumber = storeProduct.BatchNumber;
            viewModel.CostPricePerUnit = storeProduct.CostPricePerUnit;
            viewModel.Quantity = storeProduct.Quantity;
            viewModel.MRPPerUnit = storeProduct.MRPPerUnit;
            viewModel.ProductEnterDate = storeProduct.ProductEnterDate;
            viewModel.ProductId = storeProduct.Product.Id;
            viewModel.ProductName = storeProduct.Product.Name;
            viewModel.Code = storeProduct.Product.Code;
           // viewModel.SalePrice = storeProduct.SalePrice;
            if (storeProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.Batches = new SelectList(_dbContext.Batch, "Id", "Name",storeProduct.BatchId);
            ViewBag.Stores = new SelectList(_dbContext.Stores, "Id", "Name",storeProduct.Batch.StoreId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreProductsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                StoreProduct storeProduct = _dbContext.StoreProducts.Find(viewModel.Id);               
                storeProduct.ProductId = viewModel.ProductId;
                storeProduct.BatchId = viewModel.BatchId;
                storeProduct.CostPricePerUnit = viewModel.CostPricePerUnit;
                storeProduct.MRPPerUnit = storeProduct.MRPPerUnit;
                storeProduct.Quantity = viewModel.Quantity;
                _dbContext.Entry(storeProduct).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatchId = new SelectList(_dbContext.Batch, "Id", "Name", viewModel.BatchId);
            ViewBag.Stores = new SelectList(_dbContext.Stores, "Id", "Name", viewModel.StoreId);
            return View(viewModel);
        }

        public ActionResult _Create(Guid batchid)
        {
            StoreProduct storeProduct = new StoreProduct();
            storeProduct.BatchId = batchid;
            return PartialView("_Create", storeProduct);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(StoreProduct storeProduct)
        {
            if (ModelState.IsValid)
            {
                storeProduct.Id = Guid.NewGuid();
                _dbContext.StoreProducts.Add(storeProduct);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return View(storeProduct);
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


        public ActionResult _Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreProduct storeProduct = _dbContext.StoreProducts.Find(id);
            StoreProductsViewModel viewModel = new StoreProductsViewModel();
            viewModel.Id = storeProduct.Id;
            viewModel.BatchId = storeProduct.BatchId;
            viewModel.BatchNumber = storeProduct.BatchNumber;
            viewModel.CostPricePerUnit = storeProduct.CostPricePerUnit;
            viewModel.Quantity = storeProduct.Quantity;
            viewModel.MRPPerUnit = storeProduct.MRPPerUnit;
            viewModel.ProductEnterDate = storeProduct.ProductEnterDate;
            viewModel.ProductName = storeProduct.Product.Name;
            viewModel.Code = storeProduct.Product.Code;
            if (storeProduct == null)
            {
                return HttpNotFound();
            }
            string modelString = RenderRazorViewToString("_Edit", viewModel);
            //ViewBag.BatchId = new SelectList(_dbContext.Batch, "Id", "Name", storeProduct.BatchId);
            return Json(new { ModelString = modelString }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit(StoreProduct storeProduct)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(storeProduct).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatchId = new SelectList(_dbContext.Batch, "Id", "Name", storeProduct.BatchId);
            return View(storeProduct);
        }
        public JsonResult GetStoreProductsForAutocomplete(string term, Guid batchId)
        {
            Product[] productsMatching = String.IsNullOrWhiteSpace(term) ? null
                : _dbContext.Products.Where(ii => ii.Code.Contains(term) || ii.Name.Contains(term)).ToArray();
            List<StoreProductsViewModel> storeProductsViewModelList = new List<StoreProductsViewModel>();


            foreach (var storeProducts in productsMatching.ToList())
            {
                foreach (var storeProduct in storeProducts.StoreProducts.Where(b => b.BatchId == batchId))
                {
                    StoreProductsViewModel storeProductsViewModel = new StoreProductsViewModel();
                    storeProductsViewModel.Id = storeProduct.Id;
                    storeProductsViewModel.Code = storeProducts.Code;
                    storeProductsViewModel.AutoGenerateName = storeProducts.AutoGenerateName;
                    storeProductsViewModel.MRPPerUnit = storeProduct.Product.SalePrice;
                    storeProductsViewModelList.Add(storeProductsViewModel);

                }
            }

            return Json(storeProductsViewModelList.Select(m => new
            {
                Id = m.Id,
                value = m.ProductName,
                label = String.Format("{0}: {1}: {2}", m.AutoGenerateName, m.Code, m.MRPPerUnit),
                MRPPerUnit = m.MRPPerUnit,
                Name = m.ProductName,
                Code = m.Code,
                AutoGenerateName = m.AutoGenerateName,
                ModelNumber = m.ModelNumber
            }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoreProduct storeProduct = _dbContext.StoreProducts.Find(id);
            if (storeProduct == null)
            {
                return HttpNotFound();
            }
            return View(storeProduct);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            StoreProduct storeProduct = _dbContext.StoreProducts.Find(id);
            _dbContext.StoreProducts.Remove(storeProduct);
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
