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
    public class CustomerFeesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index(Guid enrollCustomerId, Guid membershipId)
        {
            var customerFees = _dbContext.CustomerFees.Where(ec => ec.EnrollCustomerId == enrollCustomerId);
            ViewBag.MembershipId = membershipId;
            ViewBag.EnrollCustomerId = enrollCustomerId;
            return PartialView("_Index", customerFees.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerFees customerFees = _dbContext.CustomerFees.Find(id);
            if (customerFees == null)
            {
                return HttpNotFound();
            }
            return View(customerFees);
        }

        public ActionResult Create(Guid enrollCustomerId, Guid membershipId)
        {
            CustomerFees customerFees = new CustomerFees();
            customerFees.EnrollCustomerId = enrollCustomerId;
            ViewBag.MembershipId = membershipId;
            return PartialView("_Create", customerFees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerFees customerFees, Guid membershipId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    decimal totalCustomerFees = 0;                   
                    var retriveCustomerFees = _dbContext.CustomerFees.Where(ec => ec.EnrollCustomerId == customerFees.EnrollCustomerId).ToList();
                    var retriveMembershipDetails = _dbContext.Memberships.Where(m => m.Id == membershipId).FirstOrDefault();
                    if (retriveCustomerFees.Count == 0)
                    {
                        customerFees.DueFees = retriveMembershipDetails.Fees - customerFees.SubmitFees;
                    }

                    if (retriveCustomerFees.Count != 0)
                    {
                        foreach (var fees in retriveCustomerFees)
                        {
                            totalCustomerFees = totalCustomerFees + fees.SubmitFees;
                        }
                        decimal tillNowFees = totalCustomerFees + customerFees.SubmitFees;
                        customerFees.DueFees = retriveMembershipDetails.Fees - tillNowFees;
                    }
                    customerFees.Id = Guid.NewGuid();
                    
                    _dbContext.CustomerFees.Add(customerFees);
                    _dbContext.SaveChanges();
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }

            ViewBag.EnrollCustomerId = new SelectList(_dbContext.EnrollCustomers, "Id", "FirstName", customerFees.EnrollCustomerId);
            return View(customerFees);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerFees customerFees = _dbContext.CustomerFees.Find(id);
            if (customerFees == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnrollCustomerId = new SelectList(_dbContext.EnrollCustomers, "Id", "FirstName", customerFees.EnrollCustomerId);
            return PartialView("_Edit", customerFees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DueFees,SubmitFees,Date,EnrollCustomerId")] CustomerFees customerFees)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(customerFees).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnrollCustomerId = new SelectList(_dbContext.EnrollCustomers, "Id", "FirstName", customerFees.EnrollCustomerId);
            return View(customerFees);
        }
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerFees customerFees = _dbContext.CustomerFees.Find(id);
            if (customerFees == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", customerFees);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CustomerFees customerFees = _dbContext.CustomerFees.Find(id);
            _dbContext.CustomerFees.Remove(customerFees);
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
