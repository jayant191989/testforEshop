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
using HR_Management.Web.BLL;
using HR_Management.Web.ViewModels;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        CompanyBLL companyBll = new CompanyBLL();

        public ActionResult Index()
        {
            return View(companyBll.GetCompanyies());
        }


        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyViewModel viewModel = companyBll.GetCompanyById(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }


        public ActionResult Create()
        {
            CompanyViewModel viewModel = new CompanyViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Id = Guid.NewGuid();
                companyBll.SaveCompany(viewModel);
                TempData["MessageToClientSuccess"] = " Successfully Created Company ";
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }


        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyViewModel viewModel = companyBll.GetCompanyById(id);
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                companyBll.EditCompany(viewModel);
                TempData["MessageToClientSuccess"] = string.Format(" Successfully Edited Company No. {0}", viewModel.Id);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companyies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            companyBll.DeleteCompany(id);
            TempData["MessageToClientSuccess"] = string.Format(" Successfully Deleted Company No. {0}", id);
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
