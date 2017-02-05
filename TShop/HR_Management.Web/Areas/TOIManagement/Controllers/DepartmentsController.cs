using HR_Management.Context;
using HR_Management.Models;
using HR_Management.Web.Helpers;
using HR_Management.Web.ViewModels;
using HR_Management.Web.DAL;
using HR_Management.Web.BLL;
using System.Web.Mvc;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Net;
using System.Data.Entity.Infrastructure;
using AutoMapper;


namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    [Authorize]
    public class DepartmentsController : DepartmentBLL
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        private UnitOfWork unitOfWork = new UnitOfWork();
        DepartmentBLL departmentBll = new DepartmentBLL();

        public ActionResult Index()
        {
            return View(departmentBll.GetDepartments());
        }

        public ActionResult DepartmentLog()
        {
            return View();
        }
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentViewModel viewModel = departmentBll.GetDepartmentById(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName");
            ViewBag.BranchId = new SelectList(_dbContext.Branches, "Id", "Name");
            DepartmentViewModel viewModel = new DepartmentViewModel();
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleModelStateException]
        public ActionResult Create(DepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelStateException(ModelState);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    departmentBll.SaveDepartment(viewModel);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbex)
                {
                    // var error = new ModelStateException(dbex);
                    Log4NetHelper.Log(String.Format("Cannot Create Deparment {0} ", viewModel.Id), LogLevel.ERROR, "Department", viewModel.Id, User.Identity.Name, dbex);
                    var msg = new ModelStateException(dbex);
                    TempData["MessageToClient"] = msg;

                }
                catch (Exception ex)
                {
                    Log4NetHelper.Log(String.Format("Cannot Create Deparment {0} ", viewModel.Id), LogLevel.ERROR, "Department", viewModel.Id, User.Identity.Name, ex);
                    var msg = new ModelStateException(ex);
                    TempData["MessageToClient"] = msg;
                }

            }
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", viewModel.CompanyId);
            ViewBag.BranchId = new SelectList(_dbContext.Branches, "Id", "Name");
            return View(viewModel);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentViewModel viewModel = departmentBll.GetDepartmentById(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", viewModel.CompanyId);
            ViewBag.BranchId = new SelectList(_dbContext.Branches, "Id", "Name");
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelStateException(ModelState);
            }
            if (ModelState.IsValid)
            {
                departmentBll.EditDepartment(viewModel);
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(_dbContext.Companyies, "Id", "CompanyName", viewModel.CompanyId);
            ViewBag.BranchId = new SelectList(_dbContext.Branches, "Id", "Name", viewModel.BranchId);
            return View(viewModel);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _dbContext.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            departmentBll.DeleteDepartment(id);
            TempData["MessageToClientSuccess"] = string.Format(" Successfully Deleted Department No. {0}", id);
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
