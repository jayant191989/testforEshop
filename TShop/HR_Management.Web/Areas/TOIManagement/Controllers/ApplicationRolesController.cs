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
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class ApplicationRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationRolesController()
        {

        }
        public ApplicationRolesController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            set
            {
                _roleManager = value;
            }
        }
        public ActionResult Index()
        {
            return View(RoleManager.Roles.ToList());
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name")] ApplicationRole applicationRole)
        {
            if (ModelState.IsValid)
            {
                var roleResult = await RoleManager.CreateAsync(applicationRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationRole);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] ApplicationRole applicationRole)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole retriveApplicationRole = await RoleManager.FindByIdAsync(applicationRole.Id);
                string orignalName = retriveApplicationRole.Name;
                if (orignalName == "EcommerceAdmin" && applicationRole.Name != "EcommerceAdmin")
                {
                    ModelState.AddModelError("", "EcommerceAdmin Role Cannot be Modified");
                    return View(applicationRole);
                }
                if (orignalName != "EcommerceAdmin" && applicationRole.Name == "EcommerceAdmin")
                {
                    ModelState.AddModelError("", "You Cannot Change the Name of EcommerceAdmin Role");
                    return View(applicationRole);
                }


                retriveApplicationRole.Name = applicationRole.Name;
                await RoleManager.UpdateAsync(retriveApplicationRole);

                return RedirectToAction("Index");
            }
            return View(applicationRole);
        }


        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
            if (applicationRole.Name == "EcommerceAdmin")
            {
                ModelState.AddModelError("", "You Cannot Delete EcommerceAdmin Role");
                return View(applicationRole);
            }

            await RoleManager.DeleteAsync(applicationRole);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RoleManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
