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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    [Authorize]
    public class ApplicationFormsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }


        private IEnumerable<ApplicationForm> GetApplicationForms(string userId, List<string> userRolesList)
        {
            IEnumerable<ApplicationForm> claimableWorkOrders = _dbContext.ApplicationForms.Where(
                ap => ap.applicationFormStatus != ApplicationForm.ApplicationFormStatus.Approved)
                .ToList()
                .Where(wo => userRolesList.Any(ur => wo.RolesWhichCanClaim.Contains(ur)));

            return claimableWorkOrders;
        }


        public ActionResult Index()
        {         
           
            var applicationForms = _dbContext.ApplicationForms.Include(a => a.Company);
            var viewModel = Mapper.Map<IEnumerable<ApplicationFormViewModel>>(applicationForms);
            //  return View(viewModel.ToList());
            return View(viewModel);
        }

        public ActionResult IndexBR()
        {
            string userId = User.Identity.GetUserId();
            List<string> userRolesList = UserManager.GetRoles(userId).ToList();
            IEnumerable<ApplicationForm> applicationFormsToDisplay = new List<ApplicationForm>();
            applicationFormsToDisplay = applicationFormsToDisplay.Concat(GetApplicationForms(userId, userRolesList));
            //  var applicationForms = _dbContext.ApplicationForms.Include(a => a.Company);
            var viewModel = Mapper.Map<IEnumerable<ApplicationFormViewModel>>(applicationFormsToDisplay);
            //  return View(viewModel.ToList());
            return View(viewModel);
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationForm applicationForm = _dbContext.ApplicationForms.Find(id);
            if (applicationForm == null)
            {
                return HttpNotFound();
            }
            return View(applicationForm);
        }

        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_dbContext.Contacts, "Id", "FullName");
            ApplicationFormViewModel viewModel = new ApplicationFormViewModel();
            viewModel.CurrentDate = DateTime.Now.Date;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationForm applicationForm = Mapper.Map<ApplicationForm>(viewModel);
                applicationForm.Id = Guid.NewGuid();
                applicationForm.CompanyId = CompanyCookie.CompId;
                _dbContext.ApplicationForms.Add(applicationForm);
                _dbContext.SaveChanges();
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
            ApplicationForm applicationForm = _dbContext.ApplicationForms.Find(id);
            ApplicationFormViewModel viewModel = Mapper.Map<ApplicationFormViewModel>(applicationForm);
            if (applicationForm == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(_dbContext.Contacts, "Id", "FullName", applicationForm.EmployeeId);

            if (viewModel.Status.Substring(viewModel.Status.Length - 3, 3) != "ing")
                return View("Forward", viewModel);

            return View(viewModel.Status, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationFormViewModel viewModel, string command)
        {
            if (ModelState.IsValid)
            {
                PromotionResult promotionResult = new PromotionResult();

                if (command == "Save")
                {
                    promotionResult.Success = true;
                    promotionResult.Message = String.Format("Changes to Leave Application {0} have been successfully saved.", viewModel.Id);
                }
                else if (command == "Forward")
                    promotionResult = viewModel.ClaimWorkListItem(User.Identity.GetUserId());
                else
                    promotionResult = viewModel.PromoteWorkListItem(command);

                ApplicationForm applicationForm = Mapper.Map<ApplicationForm>(viewModel);
                applicationForm.CompanyId = CompanyCookie.CompId;
                _dbContext.Entry(applicationForm).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(viewModel);

        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationForm applicationForm = _dbContext.ApplicationForms.Find(id);
            if (applicationForm == null)
            {
                return HttpNotFound();
            }
            return View(applicationForm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ApplicationForm applicationForm = _dbContext.ApplicationForms.Find(id);
            _dbContext.ApplicationForms.Remove(applicationForm);
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
