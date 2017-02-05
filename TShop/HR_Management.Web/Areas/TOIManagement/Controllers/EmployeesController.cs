using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using AutoMapper;
using HR_Management.Web.ViewModels;
using HR_Management.Context;
using HR_Management.Models;
using HR_Management.Web.Helpers;
using System.Data.SqlClient;


namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        [HttpPost]
        public ActionResult GetEmployee()
        {
            try
            {
                //  IEnumerable<EmployeeViewModel> viewModel;
                //EmployeeViewModel viewModel = new EmployeeViewModel();
                int filteredCount = 0;
                var employeesListCount = _dbContext.Contacts.Count();
                if (employeesListCount == 0)
                {
                    EmployeeViewModel nullViewModel = new EmployeeViewModel();
                    var resultNull = new
                    {
                        iTotalRecords = employeesListCount,
                        iTotalDisplayRecords = employeesListCount,
                        aaData = nullViewModel
                    };

                    return Json(resultNull, JsonRequestBehavior.AllowGet);
                }
                //int displayLength = iDisplayLength;
                //int displayStart = iDisplayStart;
                //int sortCol = iSortCol_0;
                //string sortDir = sSortDir_0;
                //string search = sSearch;

                //var paramDisplayLength = new SqlParameter { ParameterName = "@DisplayLength", Value = displayLength };
                //var paramDisplayStart = new SqlParameter { ParameterName = "@DisplayStart", Value = displayStart };
                //var paramSortCol = new SqlParameter { ParameterName = "@SortCol", Value = sortCol };
                //var paramSortDir = new SqlParameter { ParameterName = "@SortDir", Value = sortDir };
                //var paramSearchString = new SqlParameter { ParameterName = "@Search", Value = search };

                //  var employees = _dbContext.Database.SqlQuery<Employee>("spGetEmployees @DisplayLength ,@DisplayStart ,@SortCol ,@SortDir ,@Search", paramDisplayLength, paramDisplayStart, paramSortCol, paramSortDir, paramSearchString).ToList();
                var employees = _dbContext.Contacts.ToList();
                var viewModel = Mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

                filteredCount = viewModel.Count();
                var result = new
                {
                    iTotalRecords = filteredCount,
                    iTotalDisplayRecords = filteredCount,
                    aaData = viewModel
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["MessageToClient"] = ex;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Index()
        {
            //var employees = _dbContext.Employees.ToList();
            //return View(employees.ToList());
            return View();
        }
        public ActionResult EmployeeLog()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact employee = _dbContext.Contacts.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            // ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName");
            EmployeeViewModel viewModel = new EmployeeViewModel();
            viewModel.DepartmentList = _dbContext.Departments.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });
            viewModel.BranchList = _dbContext.Branches.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });
            viewModel.getAllGenderList = viewModel.getGenderList();
            //ViewBag.DepartmentID = new SelectList(_dbContext.Departments, "Id", "Name");
            //ViewBag.BranchId = new SelectList(_dbContext.Branches, "Id", "Name");

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel viewModel)
        {
            try
            {
                Contact employee = Mapper.Map<Contact>(viewModel);
                employee.Id  = Guid.NewGuid();
              //  employee.CompanyId = CompanyCookie.CompId;
                _dbContext.Contacts.Add(employee);
                _dbContext.SaveChanges();
                Log4NetHelper.Log(String.Format("Employee {0} created", employee.Id), LogLevel.INFO, "Employee", employee.Id, User.Identity.Name, null);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log4NetHelper.Log(String.Format("Cannot Create Employee {0} ", viewModel.Id), LogLevel.ERROR, "Employee", viewModel.Id, User.Identity.Name, ex);
            }

            Contact employeeRetrive = new Contact();
         //   ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", employeeRetrive.CompanyId);
            ViewBag.DepartmentID = new SelectList(_dbContext.Departments, "Id", "Name", employeeRetrive.DepartmentID);
            viewModel.getAllGenderList = viewModel.getGenderList();
            return View(employeeRetrive);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact employee = _dbContext.Contacts.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            EmployeeViewModel viewModel = Mapper.Map<EmployeeViewModel>(employee);
          //  ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", employee.CompanyId);
            viewModel.DepartmentList = _dbContext.Departments.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });
            viewModel.BranchList = _dbContext.Branches.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });
            viewModel.getAllGenderList = viewModel.getGenderList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Contact employee = Mapper.Map<Contact>(viewModel);
                 //   employee.CompanyId = CompanyCookie.CompId;
                    _dbContext.Entry(employee).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Log4NetHelper.Log(String.Format("Cannot Edit Employee {0} ", viewModel.Id), LogLevel.ERROR, "Employee", viewModel.Id, User.Identity.Name, ex);
                }
            }

            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", viewModel.CompanyId);
            viewModel.DepartmentList = _dbContext.Departments.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });
            viewModel.BranchList = _dbContext.Branches.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            });
            viewModel.getAllGenderList = viewModel.getGenderList();
            return View(viewModel);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact employee = _dbContext.Contacts.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Contact employee = _dbContext.Contacts.Find(id);
            _dbContext.Contacts.Remove(employee);
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
