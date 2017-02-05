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

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class BranchesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();     
        public ActionResult Index()
        {
            var branches = _dbContext.Branches.Include(b => b.Company);
            return View(branches.ToList());
        }
    
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = _dbContext.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

      
        public ActionResult Create()
        {
         //   ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                branch.Id = Guid.NewGuid();
                branch.CompanyId = CompanyCookie.CompId;
                _dbContext.Branches.Add(branch);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

          //  ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", branch.CompanyId);
            return View(branch);
        }

      
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = _dbContext.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", branch.CompanyId);
            return View(branch);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BranchCode,Name,City,State,ZipCode,CompanyId")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(branch).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", branch.CompanyId);
            return View(branch);
        }

       
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = _dbContext.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Branch branch = _dbContext.Branches.Find(id);
            _dbContext.Branches.Remove(branch);
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
