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
using HR_Management.Web.Helpers;
using System.Data.Entity.Infrastructure;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class EmployeeAttendencesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var employeeAttendences = _dbContext.EmployeeAttendences.Include(e => e.Attendence);
            return View(employeeAttendences.ToList());
        }
        public ActionResult GetEmployees(Guid? departmentId, Guid? branchId, Guid attendenceID)
        {
            if (departmentId != null && branchId != null)
            {
                var filteredEmployee = _dbContext.Contacts.Where(e => e.BranchId == branchId && e.DepartmentID == departmentId);
                var filteredViewModel = Mapper.Map<IEnumerable<EmployeeAttendenceViewModel>>(filteredEmployee);
                var employeeAttendences = _dbContext.EmployeeAttendences.Where(ea => ea.AttendenceId == attendenceID);
                //_dbContext.Dispose();

                List<EmployeeAttendenceViewModel> employeeAttendenceList = new List<EmployeeAttendenceViewModel>();
                foreach (var employee in filteredViewModel)
                {
                    EmployeeAttendenceViewModel employeeAttendenceViewModel = new EmployeeAttendenceViewModel();
                    employeeAttendenceViewModel.EmployeeId = employee.Id;
                    employeeAttendenceViewModel.FullName = employee.FullName;
                    //totalHours
                    var totalHours = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.TotalTime).FirstOrDefault();
                    if (totalHours == null)
                    {
                        employeeAttendenceViewModel.TotalTime = null;
                    }
                    else
                    {
                        employeeAttendenceViewModel.TotalTime = totalHours;
                    }
                    //overTimeHours
                    var overTimeHours = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.OverTimeHours).FirstOrDefault();
                    if (overTimeHours == null)
                    {
                        employeeAttendenceViewModel.OverTimeHours = null;
                    }
                    else
                    {
                        employeeAttendenceViewModel.OverTimeHours = overTimeHours;
                    }
                    //workHours
                    var workHours = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.WorkHours).FirstOrDefault();
                    if (workHours == null)
                    {
                        employeeAttendenceViewModel.WorkHours = null;
                    }
                    else
                    {
                        employeeAttendenceViewModel.WorkHours = workHours;
                    }

                    employeeAttendenceViewModel.AttendenceType = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.AttendenceType).FirstOrDefault();
                    //intime
                    var inTime = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.InTime).FirstOrDefault();
                    if (inTime == null)
                    {
                        employeeAttendenceViewModel.InTime = null;
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(inTime);
                        employeeAttendenceViewModel.InTime = dt;
                    }
                    //outtime
                    var outTime = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.OutTime).FirstOrDefault();
                    if (outTime == null)
                    {
                        employeeAttendenceViewModel.OutTime = null;
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(outTime);
                        employeeAttendenceViewModel.OutTime = dt;
                    }

                    employeeAttendenceViewModel.Status = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.Status).FirstOrDefault();
                    employeeAttendenceList.Add(employeeAttendenceViewModel);
                }
                return PartialView("_GetEmployees", employeeAttendenceList.ToList());
                // return PartialView("_GetEmployees", filteredViewModel);
            }
            else
            {
                var employees = _dbContext.Contacts.ToList();

                var employeesViewModel = Mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
                var employeeAttendences = _dbContext.EmployeeAttendences.Where(ea => ea.AttendenceId == attendenceID);
                //_dbContext.Dispose();

                List<EmployeeAttendenceViewModel> employeeAttendenceList = new List<EmployeeAttendenceViewModel>();
                foreach (var employee in employeesViewModel)
                {
                    EmployeeAttendenceViewModel employeeAttendenceViewModel = new EmployeeAttendenceViewModel();
                    employeeAttendenceViewModel.EmployeeId = employee.Id;
                    employeeAttendenceViewModel.FullName = employee.FullName;
                    //totalHours
                    var totalHours = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.TotalTime).FirstOrDefault();
                    if (totalHours == null)
                    {
                        employeeAttendenceViewModel.TotalTime = null;
                    }
                    else
                    {
                        employeeAttendenceViewModel.TotalTime = totalHours;
                    }
                    //overTimeHours
                    var overTimeHours = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.OverTimeHours).FirstOrDefault();
                    if (overTimeHours == null)
                    {
                        employeeAttendenceViewModel.OverTimeHours = null;
                    }
                    else
                    {
                        employeeAttendenceViewModel.OverTimeHours = overTimeHours;
                    }
                    //workHours
                    var workHours = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.WorkHours).FirstOrDefault();
                    if (workHours == null)
                    {
                        employeeAttendenceViewModel.WorkHours = null;
                    }
                    else
                    {
                        employeeAttendenceViewModel.WorkHours = workHours;
                    }
                    employeeAttendenceViewModel.AttendenceType = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.AttendenceType).FirstOrDefault();
                    //intime
                    var inTime = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.InTime).FirstOrDefault();
                    if (inTime == "")
                    {
                        employeeAttendenceViewModel.InTime = null;
                    }
                    else if (inTime == null)
                    {
                        employeeAttendenceViewModel.InTime = null;
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(inTime);
                        employeeAttendenceViewModel.InTime = dt;
                    }
                    //outtime
                    var outTime = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.OutTime).FirstOrDefault();
                    if (outTime == "")
                    {
                        employeeAttendenceViewModel.OutTime = null;
                    }
                    else if (outTime == null)
                    {
                        employeeAttendenceViewModel.OutTime = null;
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(outTime);
                        employeeAttendenceViewModel.OutTime = dt;
                    }

                    employeeAttendenceViewModel.Status = employeeAttendences.Where(e => e.FullName == employee.FullName).Select(e => e.Status).FirstOrDefault();
                    employeeAttendenceList.Add(employeeAttendenceViewModel);
                }
                return PartialView("_GetEmployees", employeeAttendenceList.ToList());
            }

        }

        [HttpPost]
        [HandleModelStateException]
        public ActionResult Save(string employeeName, string inTimeVal, string outTimeVal, Guid AttendenceId, string attendenceTypeVal, DateTime date, Guid empidval, string command)
        {
            string messageToClient = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    if (command == "Submit")
                    {
                        int overTimeCalHr = 0;
                        int th = 0;
                        EmployeeAttendence employeeAttendence = new EmployeeAttendence();
                        EmployeeSalary employeeSalary = new EmployeeSalary();
                        EmployeeSalaryDetail retriveEmployeeSalaryDetail = _dbContext.EmployeeSalaryDetails.Where(es => es.ContactId == empidval).FirstOrDefault();                     
                        if (inTimeVal=="")
                        {

                        }
                        else
                        {
                            DateTime i = Convert.ToDateTime(inTimeVal);
                            if (outTimeVal == "")
                            {

                            }
                            else
                            {
                                DateTime o = Convert.ToDateTime(outTimeVal);
                                //hour calculation
                                int inTime = i.Hour;
                                int outTime = o.Hour;
                                if (inTime == outTime)
                                {
                                    inTime = 0;
                                    outTime = 0;
                                }
                                if (inTime > 12)
                                {
                                    int newInTime = 24 - inTime;
                                    th = newInTime + outTime;
                                }
                                else
                                {
                                    th = outTime - inTime;
                                }

                                if (retriveEmployeeSalaryDetail != null)
                                {
                                    if (retriveEmployeeSalaryDetail.OverTimeCal != null)
                                    {
                                        DateTime overtimeCal = Convert.ToDateTime(retriveEmployeeSalaryDetail.OverTimeCal);
                                        overTimeCalHr = overtimeCal.Hour;
                                        //work hour and overtime hour Cal
                                        // retriving overtime and subtracting frm total hr
                                        if (th > overTimeCalHr)
                                        {
                                            int otHrcal = th - overTimeCalHr;
                                            employeeAttendence.WorkHours = overTimeCalHr;
                                            employeeSalary.WorkHours = overTimeCalHr;
                                            employeeAttendence.OverTimeHours = otHrcal;
                                            employeeSalary.OverTimeHours = otHrcal;
                                        }
                                        else
                                        {
                                            employeeAttendence.WorkHours = th;
                                            employeeSalary.WorkHours = th;
                                            employeeAttendence.OverTimeHours = 0;
                                            employeeSalary.OverTimeHours = 0;
                                        }
                                    }
                                }

                                //total Hours Cal
                                int im = i.Minute;
                                int om = o.Minute;
                                int t = im + om;
                                if (im == om)
                                {
                                    employeeAttendence.TotalTime = Convert.ToString(th);
                                }
                                else if (im > om)
                                {
                                    employeeAttendence.TotalTime = Convert.ToString(th);
                                }
                                else if (t > 60)
                                {
                                    int totalhour = th + 1;
                                    int min = t - 60;
                                    employeeAttendence.TotalTime = totalhour + " : " + min;
                                }
                                else
                                {
                                    employeeAttendence.TotalTime = th + " : " + t;
                                }
                            }
                        }
                       

                        employeeAttendence.AttendenceId = AttendenceId;
                        employeeAttendence.EmployeeId = empidval;
                        employeeAttendence.FullName = employeeName;
                        employeeAttendence.AttendenceType = attendenceTypeVal;
                        employeeAttendence.Date = date;
                        employeeAttendence.InTime = inTimeVal;
                        employeeAttendence.OutTime = outTimeVal;
                        employeeAttendence.Status = true;
                        _dbContext.EmployeeAttendences.Add(employeeAttendence);

                        //emp salary                    
                        Salary salary = _dbContext.Salary.Where(s => s.Date == date).FirstOrDefault();
                        employeeSalary.FullName = employeeName;
                        employeeSalary.RatePerHour = retriveEmployeeSalaryDetail.RatePerHour;
                        employeeSalary.RatePerHourOvertime = retriveEmployeeSalaryDetail.RatePerHourOvertime;
                        employeeSalary.Date = date;
                        employeeSalary.SalaryId = salary.Id;
                        _dbContext.EmployeeSalaries.Add(employeeSalary);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        int overTimeCalHr = 0;
                        int th = 0;
                        EmployeeAttendence employeeAttendence = _dbContext.EmployeeAttendences.Where(ea => ea.AttendenceId == AttendenceId && ea.FullName == employeeName).FirstOrDefault();
                        Salary salary = _dbContext.Salary.Where(s => s.Date == date).FirstOrDefault();
                        EmployeeSalary employeeSalary = _dbContext.EmployeeSalaries.Where(es => es.SalaryId == salary.Id && es.FullName == employeeName).FirstOrDefault();
                        if (inTimeVal == "")
                        {

                        }
                        else
                        {
                            DateTime i = Convert.ToDateTime(inTimeVal);
                            if (outTimeVal == "")
                            {

                            }
                            else
                            {
                                DateTime o = Convert.ToDateTime(outTimeVal);
                                //hour calculation
                                int inTime = i.Hour;
                                int outTime = o.Hour;
                                if (inTime == outTime)
                                {
                                    inTime = 0;
                                    outTime = 0;
                                }
                                if (inTime > 12)
                                {
                                    int newInTime = 24 - inTime;
                                    th = newInTime + outTime;
                                }
                                else
                                {
                                    th = outTime - inTime;
                                }

                                EmployeeSalaryDetail retriveEmployeeSalaryDetail = _dbContext.EmployeeSalaryDetails.Where(es => es.ContactId == empidval).FirstOrDefault();
                                if (retriveEmployeeSalaryDetail != null)
                                {
                                    if (retriveEmployeeSalaryDetail.OverTimeCal != null)
                                    {
                                        DateTime overtimeCal = Convert.ToDateTime(retriveEmployeeSalaryDetail.OverTimeCal);
                                        overTimeCalHr = overtimeCal.Hour;
                                        //work hour and overtime hour Cal
                                        // retriving overtime and subtracting frm total hr
                                        if (th > overTimeCalHr)
                                        {
                                            int otHrcal = th - overTimeCalHr;
                                            employeeAttendence.WorkHours = overTimeCalHr;
                                            employeeSalary.WorkHours = overTimeCalHr;
                                            employeeAttendence.OverTimeHours = otHrcal;
                                            employeeSalary.OverTimeHours = otHrcal;
                                        }
                                        else
                                        {
                                            employeeAttendence.WorkHours = th;
                                            employeeSalary.WorkHours = th;
                                            employeeAttendence.OverTimeHours = 0;
                                            employeeSalary.OverTimeHours = 0;
                                        }
                                    }
                                }

                                //total Hours Cal
                                int im = i.Minute;
                                int om = o.Minute;
                                int t = im + om;
                                if (im == om)
                                {
                                    employeeAttendence.TotalTime = Convert.ToString(th);
                                }
                                else if (im > om)
                                {
                                    employeeAttendence.TotalTime = Convert.ToString(th);
                                }
                                else if (t > 60)
                                {
                                    int totalhour = th + 1;
                                    int min = t - 60;
                                    employeeAttendence.TotalTime = totalhour + " : " + min;
                                }
                                else
                                {
                                    employeeAttendence.TotalTime = th + " : " + t;
                                }

                                //emp salary
                                employeeSalary.FullName = employeeName;
                                employeeSalary.RatePerHour = retriveEmployeeSalaryDetail.RatePerHour;
                                employeeSalary.RatePerHourOvertime = retriveEmployeeSalaryDetail.RatePerHourOvertime;
                                employeeSalary.Date = date;
                                employeeSalary.SalaryId = salary.Id;
                                _dbContext.Entry(employeeSalary).State = EntityState.Modified;
                                _dbContext.SaveChanges();
                            }
                        }                      

                        employeeAttendence.AttendenceId = AttendenceId;
                        employeeAttendence.EmployeeId = empidval;
                        employeeAttendence.FullName = employeeName;
                        employeeAttendence.AttendenceType = attendenceTypeVal;
                        employeeAttendence.InTime = inTimeVal;
                        employeeAttendence.OutTime = outTimeVal;
                        employeeAttendence.Status = true;
                        _dbContext.Entry(employeeAttendence).State = EntityState.Modified;

                    }
                    _dbContext.SaveChanges();
                    return Json(new { success = true });
                }
                catch (DbUpdateException dbex)
                {
                    // var error = new ModelStateException(dbex);
                    //  Log4NetHelper.Log(String.Format("Cannot Create Deparment {0} ", viewModel.Id), LogLevel.ERROR, "Department", viewModel.Id, User.Identity.Name, dbex);
                    var msg = new ModelStateException(dbex);
                    TempData["MessageToClient"] = msg;
                    messageToClient = msg.ToString();
                }
                catch (Exception ex)
                {
                    // Log4NetHelper.Log(String.Format("Cannot Create Deparment {0} ", viewModel.Id), LogLevel.ERROR, "Department", viewModel.Id, User.Identity.Name, ex);
                    var msg = new ModelStateException(ex);
                    TempData["MessageToClient"] = msg;
                }
            }

            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName");
            return View();
        }

        public ActionResult CreateEmployeeAttendence(Guid attendenceId)
        {
            var employees = _dbContext.Contacts.ToList();
            var viewModel = Mapper.Map<IEnumerable<EmployeeAttendence>>(employees);
            foreach (var employee in viewModel)
            {
                employee.AttendenceId = attendenceId;
                _dbContext.EmployeeAttendences.Add(employee);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult AttendenceByEmpId(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var empAttendence = _dbContext.EmployeeAttendences.Where(ea => ea.EmployeeId == id).OrderBy(d => d.Date.Month);
            var viewModel = Mapper.Map<IEnumerable<EmployeeAttendenceViewModel>>(empAttendence);
            if (empAttendence == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAttendence employeeAttendence = _dbContext.EmployeeAttendences.Find(id);
            if (employeeAttendence == null)
            {
                return HttpNotFound();
            }
            return View(employeeAttendence);
        }

        public ActionResult Create()
        {
            ViewBag.AttendenceId = new SelectList(_dbContext.Attendences, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeAttendence employeeAttendence)
        {
            if (ModelState.IsValid)
            {
                var employees = _dbContext.Contacts.ToList();
                var viewModel = Mapper.Map<IEnumerable<EmployeeAttendence>>(employees);
                foreach (var employee in viewModel)
                {
                    _dbContext.EmployeeAttendences.Add(employeeAttendence);
                    _dbContext.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.AttendenceId = new SelectList(_dbContext.Attendences, "Id", "Id", employeeAttendence.AttendenceId);
            return View(employeeAttendence);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAttendence employeeAttendence = _dbContext.EmployeeAttendences.Find(id);
            if (employeeAttendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttendenceId = new SelectList(_dbContext.Attendences, "Id", "Id", employeeAttendence.AttendenceId);
            return View(employeeAttendence);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InTime,OutTime,WorkHours,OverTimeHours,EmployeeId,EmployeeFullName,Status,AttendenceId")] EmployeeAttendence employeeAttendence)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(employeeAttendence).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttendenceId = new SelectList(_dbContext.Attendences, "Id", "Id", employeeAttendence.AttendenceId);
            return View(employeeAttendence);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAttendence employeeAttendence = _dbContext.EmployeeAttendences.Find(id);
            if (employeeAttendence == null)
            {
                return HttpNotFound();
            }
            return View(employeeAttendence);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeAttendence employeeAttendence = _dbContext.EmployeeAttendences.Find(id);
            _dbContext.EmployeeAttendences.Remove(employeeAttendence);
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
