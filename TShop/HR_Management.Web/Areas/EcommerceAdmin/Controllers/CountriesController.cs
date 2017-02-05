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

using TreeUtility;

namespace HR_Management.Web.Areas.EcommerceAdmin.Controllers
{
    public class CountriesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();      

        public ActionResult Index()
        {
            return View();
        }

        //  [HttpPost]
        public ActionResult GetCountries()
        {
            return View();
            // return lst;
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = _dbContext.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                country.Id = Guid.NewGuid();
                _dbContext.Countries.Add(country);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }
      

     


        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = _dbContext.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Country country = _dbContext.Countries.Find(id);
            _dbContext.Countries.Remove(country);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetCountriesForAutocomplete(string term)
        {
            Country[] matchingInventoryItems = String.IsNullOrWhiteSpace(term)
                ? null
                : _dbContext.Countries.Where(
                    ii =>
                        ii.Code.Contains(term) ||
                        ii.Name.Contains(term)).ToArray();

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
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
