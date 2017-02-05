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
    [Authorize]
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var bankAccounts = _dbContext.BankAccounts.Include(b => b.Company).Include(b => b.Contact);
            return View(bankAccounts.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = _dbContext.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName");
            ViewBag.EmployeeId = new SelectList(_dbContext.Contacts, "Id", "FirstName");
            return View();
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankName,IFSECode,AccountNumber,City,State,ZipCode,EmployeeId,CompanyId")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                _dbContext.BankAccounts.Add(bankAccount);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", bankAccount.CompanyId);
            ViewBag.EmployeeId = new SelectList(_dbContext.Contacts, "Id", "FirstName", bankAccount.ContactId);
            return View(bankAccount);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = _dbContext.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", bankAccount.CompanyId);
            ViewBag.EmployeeId = new SelectList(_dbContext.Contacts, "Id", "FirstName", bankAccount.ContactId);
            return View(bankAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankName,IFSECode,AccountNumber,City,State,ZipCode,EmployeeId,CompanyId")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(bankAccount).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", bankAccount.CompanyId);
            ViewBag.EmployeeId = new SelectList(_dbContext.Contacts, "Id", "FirstName", bankAccount.ContactId);
            return View(bankAccount);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = _dbContext.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankAccount bankAccount = _dbContext.BankAccounts.Find(id);
            _dbContext.BankAccounts.Remove(bankAccount);
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
