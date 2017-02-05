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
    public class ParticularController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
      
        public ActionResult Index()
        {
            return View(db.Particular.ToList());
        }

       
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Particular particular = db.Particular.Find(id);
            if (particular == null)
            {
                return HttpNotFound();
            }
            return View(particular);
        }

       
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult _Create()
        {
            return PartialView("_Create");           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(Particular particular)
        {
            if (ModelState.IsValid)
            {
                particular.Id = Guid.NewGuid();
                db.Particular.Add(particular);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return View(particular);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Particular particular)
        {
            if (ModelState.IsValid)
            {
                particular.Id = Guid.NewGuid();
                db.Particular.Add(particular);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(particular);
        }
    
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Particular particular = db.Particular.Find(id);
            if (particular == null)
            {
                return HttpNotFound();
            }
            return View(particular);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy,IsActive")] Particular particular)
        {
            if (ModelState.IsValid)
            {
                db.Entry(particular).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(particular);
        }

        public JsonResult GetParticularForAutocomplete(string term)
        {
            Particular[] productsMatching = String.IsNullOrWhiteSpace(term) ? null
                : db.Particular.Where(ii => ii.Name.Contains(term)).ToArray();

            return Json(productsMatching.Select(m => new
            {
                Id = m.Id,
                value = m.Name,
                label = String.Format("{0}", m.Name),
                Name = m.Name               
            }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Particular particular = db.Particular.Find(id);
            if (particular == null)
            {
                return HttpNotFound();
            }
            return View(particular);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Particular particular = db.Particular.Find(id);
            db.Particular.Remove(particular);
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
