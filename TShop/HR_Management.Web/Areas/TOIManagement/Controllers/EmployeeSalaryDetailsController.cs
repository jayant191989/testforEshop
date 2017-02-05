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
using AutoMapper;
using HR_Management.Web.ViewModels;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class EmployeeSalaryDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        //public ActionResult Create(EmployeeSalaryDetailViewModel viewModel)
        //{
        //   // EmployeeSalaryDetailViewModel viewModel = new EmployeeSalaryDetailViewModel();
        //    var newViewModel = viewModel;
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName");
        //    return View(viewModel);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeSalaryDetailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                EmployeeSalaryDetail employeeSalaryDetail = new EmployeeSalaryDetail();
                employeeSalaryDetail.Id = viewModel.Id;
                employeeSalaryDetail.RatePerHour = viewModel.RatePerHour;
                employeeSalaryDetail.RatePerHourOvertime = viewModel.RatePerHourOvertime;
                employeeSalaryDetail.ContactId = viewModel.EmployeeId;
                employeeSalaryDetail.PF = viewModel.PF;
                employeeSalaryDetail.ESI = viewModel.ESI;
                employeeSalaryDetail.Enrolled = viewModel.Enrolled;
                if (viewModel.OverTimeCal == null)
                {
                    employeeSalaryDetail.OverTimeCal = null;
                }
                else
                {
                    employeeSalaryDetail.OverTimeCal = viewModel.OverTimeCal.Value.ToString();
                }
                db.EmployeeSalaryDetails.Add(employeeSalaryDetail);
                db.SaveChanges();
                TempData["MessageToClientSuccess"] = "Succesfully Saved Employee Salary Detail";
                return RedirectToAction("Index", "Employees");
            }
            return View(viewModel);
        }

        public ActionResult Edit(Guid? Id)
        {
            EmployeeSalaryDetail retriveEmployeeSalaryDetail = db.EmployeeSalaryDetails.Where(es => es.ContactId == Id).FirstOrDefault();
            if (retriveEmployeeSalaryDetail == null)
            {
                EmployeeSalaryDetail employeeSalaryDetail = new EmployeeSalaryDetail();
                EmployeeSalaryDetailViewModel empSalViewModel = Mapper.Map<EmployeeSalaryDetailViewModel>(employeeSalaryDetail);
                empSalViewModel.EmployeeId = Id;
                return View("Create", empSalViewModel);
            }

            EmployeeSalaryDetailViewModel viewModel = new EmployeeSalaryDetailViewModel();
            viewModel.Id = retriveEmployeeSalaryDetail.Id;
            viewModel.RatePerHour = retriveEmployeeSalaryDetail.RatePerHour;
            viewModel.RatePerHourOvertime = retriveEmployeeSalaryDetail.RatePerHourOvertime;
            viewModel.EmployeeId = retriveEmployeeSalaryDetail.ContactId;
            viewModel.PF = retriveEmployeeSalaryDetail.PF;
            viewModel.ESI = retriveEmployeeSalaryDetail.ESI;
            viewModel.Enrolled = retriveEmployeeSalaryDetail.Enrolled;
            if (retriveEmployeeSalaryDetail.OverTimeCal == null)
            {
                retriveEmployeeSalaryDetail.OverTimeCal = null;
            }
            else
            {

                var dt = TimeSpan.Parse(retriveEmployeeSalaryDetail.OverTimeCal);
                viewModel.OverTimeCal = dt;
            }

            return RedirectToAction("EditSave", viewModel);
        }
        public ActionResult EditSave(EmployeeSalaryDetailViewModel viewModel)
        {
            EmployeeSalaryDetail employeeSalaryDetail = Mapper.Map<EmployeeSalaryDetail>(viewModel);
            return View("EditSave", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeSalaryDetailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EmployeeSalaryDetail employeeSalaryDetail = Mapper.Map<EmployeeSalaryDetail>(viewModel);
                    db.Entry(employeeSalaryDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MessageToClientSuccess"] = "Succesfully Updated Employee Salary Detail";
                    return RedirectToAction("Index", "Employees");
                }
                catch (Exception ex)
                {
                    // Log4NetHelper.Log(String.Format("Cannot Create Deparment {0} ", viewModel.Id), LogLevel.ERROR, "Department", viewModel.Id, User.Identity.Name, ex);
                    var msg = ex.Message;
                    TempData["MessageToClientError"] = msg;
                }
            }
            // ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeSalaryDetail.EmployeeId);
            return View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSalaryDetail employeeSalaryDetail = db.EmployeeSalaryDetails.Find(id);
            if (employeeSalaryDetail == null)
            {
                return HttpNotFound();
            }
            return View(employeeSalaryDetail);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeSalaryDetail employeeSalaryDetail = db.EmployeeSalaryDetails.Find(id);
            db.EmployeeSalaryDetails.Remove(employeeSalaryDetail);
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
