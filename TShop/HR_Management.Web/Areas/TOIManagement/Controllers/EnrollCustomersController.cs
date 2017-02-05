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
using AutoMapper;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class EnrollCustomersController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index(Guid? membershipId)
        {
            var enrollCustomers = _dbContext.EnrollCustomers.Where(p => p.MembershipId == membershipId).OrderBy(p => p.FirstName);
            ViewBag.MembershipId = membershipId;
            return PartialView("_Index", enrollCustomers.ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCustomer enrollCustomer = _dbContext.EnrollCustomers.Find(id);
            if (enrollCustomer == null)
            {
                return HttpNotFound();
            }
            return View(enrollCustomer);
        }

        public ActionResult Create(Guid membershipId)
        {
            EnrollCustomerViewModel viewModel = new EnrollCustomerViewModel();
            viewModel.MembershipId = membershipId;
            //ViewBag.MembershipId = new SelectList(_dbContext.Memberships, "Id", "MembershipName");
            return PartialView("_Create", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EnrollCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                EnrollCustomer enrollCustomer = Mapper.Map<EnrollCustomer>(viewModel);
                enrollCustomer.Id = Guid.NewGuid();
                var membership = _dbContext.Memberships.Where(m => m.Id == enrollCustomer.MembershipId).FirstOrDefault();
                DateTime? dateOfJoining = enrollCustomer.DateOfJoining;
                int timePeriod = Convert.ToInt16(membership.TimePeriod);
                if (timePeriod == 1)
                {
                    DateTime? endMembershipTime = dateOfJoining.Value.AddMonths(1);
                    enrollCustomer.MembershipEndTime = endMembershipTime;
                }
                else if (timePeriod == 2)
                {
                    DateTime? endMembershipTime = dateOfJoining.Value.AddMonths(3);
                    enrollCustomer.MembershipEndTime = endMembershipTime;
                }
                else if (timePeriod == 3)
                {
                    DateTime? endMembershipTime = dateOfJoining.Value.AddMonths(6);
                    enrollCustomer.MembershipEndTime = endMembershipTime;
                }
                else if (timePeriod == 4)
                {
                    DateTime? endMembershipTime = dateOfJoining.Value.AddMonths(9);
                    enrollCustomer.MembershipEndTime = endMembershipTime;
                }
                else if (timePeriod == 5)
                {
                    DateTime? endMembershipTime = dateOfJoining.Value.AddMonths(12);
                    enrollCustomer.MembershipEndTime = endMembershipTime;
                }
                else if (timePeriod == 6)
                {
                    DateTime? endMembershipTime = dateOfJoining.Value.AddMonths(24);
                    enrollCustomer.MembershipEndTime = endMembershipTime;
                }

                _dbContext.EnrollCustomers.Add(enrollCustomer);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }

            // ViewBag.MembershipId = new SelectList(_dbContext.Memberships, "Id", "MembershipName", enrollCustomer.MembershipId);
            return View(viewModel);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCustomer enrollCustomer = _dbContext.EnrollCustomers.Find(id);
            if (enrollCustomer == null)
            {
                return HttpNotFound();
            }
            ViewBag.MembershipId = new SelectList(_dbContext.Memberships, "Id", "MembershipName", enrollCustomer.MembershipId);
            return View(enrollCustomer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EnrollCustomer enrollCustomer)
        {
            if (ModelState.IsValid)
            {
                enrollCustomer.DateOfJoining = enrollCustomer.EnrolledDate;
                _dbContext.Entry(enrollCustomer).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Memberships");
            }
            ViewBag.MembershipId = new SelectList(_dbContext.Memberships, "Id", "MembershipName", enrollCustomer.MembershipId);
            return View(enrollCustomer);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCustomer enrollCustomer = _dbContext.EnrollCustomers.Find(id);
            if (enrollCustomer == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", enrollCustomer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EnrollCustomer enrollCustomer = _dbContext.EnrollCustomers.Find(id);
            _dbContext.EnrollCustomers.Remove(enrollCustomer);
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
