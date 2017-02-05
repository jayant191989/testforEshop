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
using System.Web.Script.Serialization;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class SalariesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var salaries = _dbContext.Salary.Include(s => s.Company).ToList();
            foreach (var salary in salaries)
            {
                decimal? workHourTotal = 0;
                decimal? overTimeTotal = 0;
                decimal? grandTotal = 0;
                var empSalaries = _dbContext.EmployeeSalaries.Where(es => es.Date == salary.Date).ToList();
                foreach (var empSalary in empSalaries)
                {
                    workHourTotal = empSalary.WorkHourTotal + workHourTotal;
                    overTimeTotal = empSalary.OverTimeTotal + overTimeTotal;
                }
                grandTotal = workHourTotal + overTimeTotal;
                salary.DaySalary = grandTotal;
            }
            var viewModel = Mapper.Map<IEnumerable<SalaryViewModel>>(salaries);
            return View(viewModel.ToList());
        }

        public ActionResult SalaryByEmployees()
        {
            var employees = _dbContext.Contacts.ToList();
            var viewModel = Mapper.Map<IEnumerable<EmployeeSalaryByViewModel>>(employees);
            //  EmployeeSalaryByViewModel employeeSalaryByViewModel = new EmployeeSalaryByViewModel();
            // int total = 0;

            foreach (var employee in viewModel)
            {
                // var month="";
                decimal? grandTotalWorkHours = 0;
                decimal? grandTotalOverTimeHours = 0;
                List<SalaryByEmployeeViewModel> salaryByEmpViewModelList = new List<SalaryByEmployeeViewModel>();
                var employeeSalaryDetails = _dbContext.EmployeeSalaryDetails.Where(es => es.ContactId == employee.Id).FirstOrDefault();
                var salaries = _dbContext.EmployeeAttendences.Where(es => es.FullName == employee.FullName && es.Date.Month == DateTime.Now.Month).OrderBy(a => a.Date).ToList();
                foreach (var salary in salaries)
                {
                    SalaryByEmployeeViewModel salaryByEmpViewModel = new SalaryByEmployeeViewModel();
                    salaryByEmpViewModel.Date = salary.Date;
                    salaryByEmpViewModel.FullName = salary.FullName;
                    salaryByEmpViewModel.AttendenceType = salary.AttendenceType;

                    var workHours = salary.WorkHours;
                    if (workHours == null)
                    {

                    }
                    else
                    {
                        salaryByEmpViewModel.WorkHours = workHours;
                        var overTimeHours = salary.OverTimeHours;
                        salaryByEmpViewModel.OverTimeHours = overTimeHours;

                        var ratePerHour = employeeSalaryDetails.RatePerHour;
                        salaryByEmpViewModel.RatePerHour = ratePerHour;
                        var totalWorkHour = ratePerHour * workHours;
                        salaryByEmpViewModel.WorkHourTotal = totalWorkHour;

                        var rateOverTime = employeeSalaryDetails.RatePerHourOvertime;
                        salaryByEmpViewModel.RatePerHourOvertime = rateOverTime;
                        var totalOverTime = overTimeHours * rateOverTime;
                        salaryByEmpViewModel.OverTimeTotal = totalOverTime;

                        grandTotalOverTimeHours = totalOverTime + grandTotalOverTimeHours;
                        grandTotalWorkHours = totalWorkHour + grandTotalWorkHours;
                    }


                    salaryByEmpViewModelList.Add(salaryByEmpViewModel);
                    employee.Month = salary.Date.Month;
                }

                employee.Total = grandTotalOverTimeHours + grandTotalWorkHours;
                // IEnumerable<SalaryByEmployeeViewModel> en = salaryByEmpViewModelList;
                employee.SalaryByEmployeeViewModel = salaryByEmpViewModelList;
                // employeeSalaryByViewModel.SalaryByEmployeeViewModel = en;
            }

            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = _dbContext.Salary.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        public ActionResult Edit(Guid? id, DateTime? date)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeAttendences = _dbContext.EmployeeAttendences.Where(a => a.Date == date).ToList();
            if (employeeAttendences == null)
            {
                return HttpNotFound();
            }
            SalaryViewModel salaryViewModel = new SalaryViewModel();
            salaryViewModel.Date = date;
            decimal? grandTotalWorkHours = 0;
            decimal? grandTotalOverTimeHours = 0;
            List<EmployeeSalaryViewModel> viewModelList = new List<EmployeeSalaryViewModel>();
            foreach (var attendence in employeeAttendences)
            {
                EmployeeSalaryViewModel empSalaryViewModel = new EmployeeSalaryViewModel();
                var workHours = attendence.WorkHours;
                empSalaryViewModel.WorkHours = workHours;
                var overTimeHours = attendence.OverTimeHours;
                empSalaryViewModel.OverTimeHours = overTimeHours;

                var empId = attendence.EmployeeId;
                var employee = _dbContext.Contacts.Where(a => a.Id == empId).FirstOrDefault();
                var employeeSalaryDetails = _dbContext.EmployeeSalaryDetails.Where(a => a.ContactId == empId).FirstOrDefault();
                if (employeeSalaryDetails == null)
                {
                    empSalaryViewModel.RatePerHour = null;
                    empSalaryViewModel.RatePerHourOvertime = null;
                    empSalaryViewModel.FullName = employee.FullName;
                }
                else
                {
                    var ratePerHour = employeeSalaryDetails.RatePerHour;
                    empSalaryViewModel.RatePerHour = ratePerHour;
                    var totalWorkHour = ratePerHour * workHours;
                    empSalaryViewModel.WorkHourTotal = totalWorkHour;

                    var rateOverTime = employeeSalaryDetails.RatePerHourOvertime;
                    empSalaryViewModel.RatePerHourOvertime = rateOverTime;
                    var totalOverTime = overTimeHours * rateOverTime;
                    empSalaryViewModel.OverTimeTotal = totalOverTime;

                    grandTotalOverTimeHours = totalOverTime + grandTotalOverTimeHours;
                    grandTotalWorkHours = totalWorkHour + grandTotalWorkHours;

                    empSalaryViewModel.FullName = employee.FullName;
                }
                salaryViewModel.GrandTotalWorkHours = grandTotalWorkHours;
                salaryViewModel.GrandTotalOverTimeHours = grandTotalOverTimeHours;
                // empSalaryViewModel.RatePerHour =;
                viewModelList.Add(empSalaryViewModel);
                salaryViewModel.EmployeeSalaryViewModel = viewModelList;
            }

            return View(salaryViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,DaySalary,CompanyId")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(salary).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", salary.CompanyId);
            return View(salary);
        }

        [HttpPost]
        public ActionResult Save(EmployeeSalaryViewModel obj)
        {
            // IEnumerable<EmployeeSalaryViewModel> en = customerDueFeesList;
            // en = jss.Deserialize<IEnumerable<EmployeeSalaryViewModel>>(obj.EmployeeSalaryViewModelList);
            List<EmployeeSalaryViewModel> employeeSalaryViewModelList;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            EmployeeSalary employeeSalary = new EmployeeSalary();
            employeeSalaryViewModelList = jss.Deserialize<List<EmployeeSalaryViewModel>>(obj.SalaryList);
            foreach (var employeeSalaryViewModel in employeeSalaryViewModelList)
            {
                if (employeeSalaryViewModel.FullName == null)
                {
                    //for the last row of table
                }
                else
                {
                    employeeSalary.FullName = employeeSalaryViewModel.FullName;
                    employeeSalary.WorkHours = employeeSalaryViewModel.WorkHours;
                    employeeSalary.RatePerHour = employeeSalaryViewModel.RatePerHour;
                    employeeSalary.OverTimeHours = employeeSalaryViewModel.OverTimeHours;
                    employeeSalary.RatePerHourOvertime = employeeSalaryViewModel.RatePerHourOvertime;
                    employeeSalary.Date = employeeSalaryViewModel.Date;
                    employeeSalary.SalaryId = employeeSalaryViewModel.SalaryId;
                    _dbContext.EmployeeSalaries.Add(employeeSalary);
                    _dbContext.SaveChanges();
                }
            }
            return Json(new { success = true });
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = _dbContext.Salary.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary salary = _dbContext.Salary.Find(id);
            _dbContext.Salary.Remove(salary);
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
