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
using HR_Management.Web.ViewModels;
using AutoMapper;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class MembershipsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var memberships = _dbContext.Memberships.Include(m => m.Company);
            return View(memberships.ToList());
        }


        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membership membership = _dbContext.Memberships.Find(id);
            if (membership == null)
            {
                return HttpNotFound();
            }
            return View(membership);
        }


        public ActionResult Create()
        {
            MembershipViewModel viewModel = new MembershipViewModel();
            viewModel.getAllDaysList = viewModel.getAllWeekDaysList();
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MembershipViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Membership membership = Mapper.Map<Membership>(viewModel);
                membership.Id = Guid.NewGuid();
                membership.CompanyId = CompanyCookie.CompId;
                _dbContext.Memberships.Add(membership);
                _dbContext.SaveChanges();
                TempData["MessageToClientSuccess"] = "SuccessFully Saved";
                return RedirectToAction("Index");
            }
            viewModel.getAllDaysList = viewModel.getAllWeekDaysList();
            return View(viewModel);
        }


        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membership membership = _dbContext.Memberships.Find(id);
            MembershipViewModel viewModel = Mapper.Map<MembershipViewModel>(membership);
            if (membership == null)
            {
                return HttpNotFound();
            }
            viewModel.getAllDaysList = viewModel.getAllWeekDaysList();
            //ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", membership.CompanyId);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MembershipViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Membership membership = Mapper.Map<Membership>(viewModel);
                    membership.CompanyId = CompanyCookie.CompId;
                    _dbContext.Entry(membership).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    TempData["MessageToClientSuccess"] = "SuccessFully Saved";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var msg = new ModelStateException(ex);
                    TempData["MessageToClientError"] = msg;
                    return View(viewModel);
                }
            }

            //   ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", membership.CompanyId);
            viewModel.getAllDaysList = viewModel.getAllWeekDaysList();
            return View(viewModel);
        }


        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membership membership = _dbContext.Memberships.Find(id);
            if (membership == null)
            {
                return HttpNotFound();
            }
            return View(membership);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HandleModelStateException]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                Membership membership = _dbContext.Memberships.Find(id);
                _dbContext.Memberships.Remove(membership);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //  Log4NetHelper.Log(String.Format("Cannot Create Deparment {0} ", viewModel.Id), LogLevel.ERROR, "Department", viewModel.Id, User.Identity.Name, ex);
                var msg = new ModelStateException(ex);
                TempData["MessageToClientError"] = msg;
            }
            return RedirectToAction("Delete");
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
