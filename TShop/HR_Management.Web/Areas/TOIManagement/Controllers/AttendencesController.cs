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
using HR_Management.Web.Helpers;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    [Authorize]
    public class AttendencesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var attendences = _dbContext.Attendences.Include(a => a.Company).OrderBy(a=>a.Date);
            return View(attendences.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = _dbContext.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }


        public ActionResult Create(int? departmentId, int? branchId)
        {
            ViewBag.Branches = new SelectList(_dbContext.Branches, "Id", "Name");
            ViewBag.Departments = new SelectList(_dbContext.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttendenceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Attendence attendence = Mapper.Map<Attendence>(viewModel);
                    attendence.Id = Guid.NewGuid();
                    attendence.CompanyId = CompanyCookie.CompId;
                    _dbContext.Attendences.Add(attendence);
                    _dbContext.SaveChanges();

                    Salary salary = new Salary();
                    salary.CompanyId = CompanyCookie.CompId;
                    salary.Date = viewModel.Date;
                    _dbContext.Salary.Add(salary);
                    _dbContext.SaveChanges();
                    TempData["MessageToClientSuccess"] = "Attendence Date SuccessFully Created";

                }
                catch (Exception ex)
                {
                    // Log4NetHelper.Log(String.Format("Cannot Create Deparment {0} ", viewModel.Id), LogLevel.ERROR, "Department", viewModel.Id, User.Identity.Name, ex);
                    var msg = new ModelStateException(ex);
                    TempData["MessageToClientError"] = msg;
                }

                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName");
            return View(viewModel);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Attendence attendence = _dbContext.Attendences.Find(id);
            //_dbContext.Dispose();
            //_dbContext = new ApplicationDbContext();
            //SaveEmpAttendence(attendence.Id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.Branches = new SelectList(_dbContext.Branches, "Id", "Name");
            ViewBag.Departments = new SelectList(_dbContext.Departments, "Id", "Name");
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", attendence.CompanyId);
            return View(attendence);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttendenceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Attendence attendence = Mapper.Map<Attendence>(viewModel);
                _dbContext.Entry(attendence).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName");
            return View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = _dbContext.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendence attendence = _dbContext.Attendences.Find(id);
            _dbContext.Attendences.Remove(attendence);
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

        [HttpPost]
        public ActionResult GetDepartments(Guid Id)
        {
            var departments = _dbContext.Departments.Where(d => d.BranchId == Id);
            return Json(new
            {
                DepartmentList = new SelectList(departments, "Id", "Name"),
                JsonRequestBehavior.AllowGet
            });
        }
    }
}
