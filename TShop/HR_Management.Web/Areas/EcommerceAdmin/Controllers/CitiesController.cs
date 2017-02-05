using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Management.Context;
using HR_Management.Model.Models;

namespace HR_Management.Web.Areas.EcommerceAdmin.Controllers
{
    public class CitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
      
        public ActionResult Index()
        {
            var cities = db.Cities.Include(c => c.State);
            return View(cities.ToList());
        }
       
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }
      
        public ActionResult Create()
        {
            ViewBag.StateId = new SelectList(db.States, "Id", "Name");
           // ViewBag.CountryId = new SelectList(db.States, "Id", "Name");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,ZipCode,StateId")] City city)
        {
            if (ModelState.IsValid)
            {
                city.Id = Guid.NewGuid();
                db.Cities.Add(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateId = new SelectList(db.States, "Id", "Name", city.StateId);
            return View(city);
        }
     
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", city.StateId);
            return View(city);
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,ZipCode,StateId")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", city.StateId);
            return View(city);
        }
       
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            City city = db.Cities.Find(id);
            db.Cities.Remove(city);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetCitiesForAutocomplete(string term, string state)
        {
            City[] matchingInventoryItems = String.IsNullOrWhiteSpace(term)
                ? null
                : db.Cities.Where(s => s.State.Name == state).Where(ii => ii.Code.Contains(term) || ii.Name.Contains(term)).ToArray();

            return Json(matchingInventoryItems.Select(m => new
            {
                id = m.Id,
                value = m.Name,
                label = String.Format("{0}: {1}", m.Code, m.Name),
                Code = m.Code,
                Name = m.Name
            }), JsonRequestBehavior.AllowGet);
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
