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
using HR_Management.Web.Helpers;
using System.Web.Hosting;
using System.IO;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class ProductImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        FilesHelper filesHelper;
        String tempPath = "~/Products/";
        String serverMapPath = "~/Images/Upload/Products/";
        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }
        private string UrlBase = "/Images/Upload/Products/";
        String DeleteURL = "/ProductImages/DeleteFile/?file=";
        String DeleteType = "GET";
        public ProductImagesController()
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }      

        [HttpPost]
        public JsonResult Upload(Guid productId)
        {
            var resultList = new List<ViewDataUploadFilesResult>();
            var CurrentContext = HttpContext;

            filesHelper.UploadAndShowResults(CurrentContext, resultList, productId);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                return Json(files);
            }
        }

        public ActionResult GetImages(Guid? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(Id);
            ViewDataUploadFilesResult viewDataUploadFilesResult = new ViewDataUploadFilesResult();
            viewDataUploadFilesResult.productId = product.Id;
            var productImages = product.ProductImages.ToList();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(viewDataUploadFilesResult);
        }

        public JsonResult GetFileList(Guid productId)
        {
            var list = filesHelper.GetFileList(productId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFile(string file,Guid imageId)
        {
            filesHelper.DeleteFile(file, imageId);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productImage);
        }

    
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProductImage productImage = db.ProductImages.Find(id);
            db.ProductImages.Remove(productImage);
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
