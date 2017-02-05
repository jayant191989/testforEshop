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
using System.IO;
using ImageResizer;
using HR_Management.Web.Helpers;
using System.Data.Entity.Infrastructure;

namespace HR_Management.Web.Areas.TOIManagement.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index(string status)
        {
            //if (status == "showall")
            //{
            //    var allCustomers = _dbContext.Contacts.Include(c => c.Company);
            //    return View(allCustomers.ToList());
            //}
            //var customers = _dbContext.Contacts.Include(c => c.Company).Where(c => c.Status == true);
            var allCustomers = _dbContext.Contacts.ToList();
            return View(allCustomers.ToList());
        }

        public ActionResult ChangeStatus(Guid id)
        {
            Contact Contact = _dbContext.Contacts.Find(id);
            if (Contact.Status == true)
            {
                Contact.Status = false;
            }
            else if (Contact.Status == false)
            {
                Contact.Status = true;
            }
            _dbContext.Entry(Contact).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact customer = _dbContext.Contacts.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult Create()
        {
            CustomerViewModel viewModel = new CustomerViewModel();
            viewModel.getAllGenderList = viewModel.getGenderList();
            viewModel.getAllCustomerTypeList = viewModel.getCustomerTypeList();
            ViewBag.BranchId = new SelectList(_dbContext.Branches, "Id", "Name");
            ViewBag.DepartmentID = new SelectList(_dbContext.Departments, "Id", "Name");
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //  Product product = Mapper.Map<Product>(viewModel);
                    Contact customer = Mapper.Map<Contact>(viewModel);
                    customer.Id = Guid.NewGuid();
                    if (viewModel.MainImageNameFile != null)
                    {
                        string fName = "";
                        HttpPostedFileBase file = viewModel.MainImageNameFile;
                        fName = viewModel.MainImageNameFile.FileName;
                        string ImageNameWithOutExtention = System.IO.Path.GetFileNameWithoutExtension(fName);
                        string extension = Path.GetExtension(fName);

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Customers", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");
                        var fileName1 = Path.GetFileName(file.FileName);
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);
                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);


                        var versions = new Dictionary<string, string>();
                        var imagePath = string.Format("{0}\\{1}", pathString, ImageNameWithOutExtention);

                        versions.Add("_small", "maxwidth=100&maxheight=100&format=jpg");
                        versions.Add("_medium", "maxwidth=500&maxheight=500&format=jpg");
                        versions.Add("_large", "maxwidth=900&maxheight=900&format=jpg");
                        foreach (var suffix in versions.Keys)
                        {
                            file.InputStream.Seek(0, SeekOrigin.Begin);
                            ImageBuilder.Current.Build(
                                new ImageJob(
                                    file.InputStream,
                                   imagePath + suffix,
                                    new Instructions(versions[suffix]),
                                    false,
                                    true));
                        }

                        customer.MainImageName = ImageNameWithOutExtention;
                        customer.ImageExtention = extension;
                    }
                    customer.Type = "Customer";
                    if (viewModel.OpeningBalance == null)
                    {
                        customer.OpeningBalance = 0;
                    }
                    customer.OpeningBalance = viewModel.OpeningBalance;
                   // customer.CompanyId = CompanyCookie.CompId;
                    _dbContext.Contacts.Add(customer);
                    _dbContext.SaveChanges();
                    TempData["MessageToClientSuccess"] = "SuccessFully Saved";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException dbex)
                {
                    // var error = new ModelStateException(dbex);
                    // Log4NetHelper.Log(String.Format("Cannot Create Deparment {0} ", viewModel.Id), LogLevel.ERROR, "Department", viewModel.Id, User.Identity.Name, dbex);
                    var msg = new ModelStateException(dbex);
                    TempData["MessageToClientError"] = msg;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.MembershipId = new SelectList(_dbContext.Memberships, "Id", "MembershipName");
                    viewModel.getAllGenderList = viewModel.getGenderList();
                    viewModel.getAllCustomerTypeList = viewModel.getCustomerTypeList();
                    return View(viewModel);
                }
            }
            viewModel.getAllGenderList = viewModel.getGenderList();
            viewModel.getAllCustomerTypeList = viewModel.getCustomerTypeList();
            //  ViewBag.MembershipId = new SelectList(_dbContext.Memberships, "Id", "MembershipName", customer.MembershipId);
            return View(viewModel);
        }

        public ActionResult _Create()
        {
            CustomerViewModel viewModel = new CustomerViewModel();
            viewModel.getAllGenderList = viewModel.getGenderList();
            viewModel.getAllCustomerTypeList = viewModel.getCustomerTypeList();
            ViewBag.BranchId = new SelectList(_dbContext.Branches, "Id", "Name");
            ViewBag.DepartmentID = new SelectList(_dbContext.Departments, "Id", "Name");
            return PartialView("_Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Contact customer = Mapper.Map<Contact>(viewModel);
                    customer.Id = Guid.NewGuid();
                    if(viewModel.CustomerType == "1")
                    {
                        customer.Type = "Company";
                    }
                   else if (viewModel.CustomerType == "3")
                    {
                        customer.Type = "Employee";
                    }
                    else
                    {
                        customer.Type = "Customer";
                    }
                    // customer.CompanyId = CompanyCookie.CompId;

                    _dbContext.Contacts.Add(customer);
                    _dbContext.SaveChanges();
                    TempData["MessageToClientSuccess"] = "SuccessFully Saved";
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            viewModel.getAllGenderList = viewModel.getGenderList();
            viewModel.getAllCustomerTypeList = viewModel.getCustomerTypeList();
            return View(viewModel);

        }
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact customer = _dbContext.Contacts.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel viewModel = Mapper.Map<CustomerViewModel>(customer);
            viewModel.getAllGenderList = viewModel.getGenderList();
            viewModel.getAllCustomerTypeList = viewModel.getCustomerTypeList();
            ViewBag.BranchIdName = new SelectList(_dbContext.Branches, "Id", "Name", viewModel.BranchId);
            ViewBag.DepartmentName = new SelectList(_dbContext.Departments, "Id", "Name", viewModel.DepartmentID);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Contact customer = Mapper.Map<Contact>(viewModel);
                if (viewModel.MainImageNameFile != null)
                {
                    string fName = "";
                    HttpPostedFileBase file = viewModel.MainImageNameFile;
                    fName = viewModel.MainImageNameFile.FileName;
                    string ImageNameWithOutExtention = System.IO.Path.GetFileNameWithoutExtension(fName);
                    string extension = Path.GetExtension(fName);

                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Customers", Server.MapPath(@"\")));
                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");
                    var fileName1 = Path.GetFileName(file.FileName);
                    bool isExists = System.IO.Directory.Exists(pathString);
                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);
                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);


                    var versions = new Dictionary<string, string>();
                    var imagePath = string.Format("{0}\\{1}", pathString, ImageNameWithOutExtention);

                    versions.Add("_small", "maxwidth=100&maxheight=100&format=jpg");
                    versions.Add("_medium", "maxwidth=500&maxheight=500&format=jpg");
                    versions.Add("_large", "maxwidth=900&maxheight=900&format=jpg");
                    foreach (var suffix in versions.Keys)
                    {
                        file.InputStream.Seek(0, SeekOrigin.Begin);
                        ImageBuilder.Current.Build(
                            new ImageJob(
                                file.InputStream,
                               imagePath + suffix,
                                new Instructions(versions[suffix]),
                                false,
                                true));
                    }

                    customer.MainImageName = ImageNameWithOutExtention;
                    customer.ImageExtention = extension;
                }

                //   customer.CompanyId = CompanyCookie.CompId;
                customer.BranchId = viewModel.BranchId;
                customer.DepartmentID = viewModel.DepartmentID;
                if (viewModel.OpeningBalance == null)
                {
                    customer.OpeningBalance = 0;
                }
                customer.OpeningBalance = viewModel.OpeningBalance;
                _dbContext.Entry(customer).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            viewModel.getAllGenderList = viewModel.getGenderList();
            viewModel.getAllCustomerTypeList = viewModel.getCustomerTypeList();
            return View(viewModel);
        }
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact customer = _dbContext.Contacts.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Contact customer = _dbContext.Contacts.Find(id);
            _dbContext.Contacts.Remove(customer);
            //  customer.Status = false;
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

        public JsonResult GetCustomersForAutocomplete(string term, Guid? membershipId)
        {
            Contact[] customersMatching = String.IsNullOrWhiteSpace(term) ? null
                : _dbContext.Contacts.Where(ii => ii.FirstName.Contains(term) || ii.LastName.Contains(term)).Where(c => c.Status == true).ToArray();
            List<CustomerViewModel> viewModelList = new List<CustomerViewModel>();
            foreach (var contact in customersMatching)
            {
                CustomerViewModel customerViewModel = new CustomerViewModel();
                var contactDues = _dbContext.Daily.
                    Where(d => d.ContactId == contact.Id).OrderBy(d => d.Invoice).OrderBy(d=>d.CreatedDate).ToList();

                //decimal? total = 0;
                //decimal? totalAmtTaken = 0;
                //decimal? outStanding = 0;

                if (contactDues.Count == 0)
                {
                    customerViewModel.OpeningBalance = contact.OpeningBalance;
                }
                else
                {
                    //foreach (var contactPayment in contactDues)
                    //{
                    //    total = contactPayment.DailyTotal + total;
                    //    totalAmtTaken = contactPayment.Credit + totalAmtTaken;
                    //}
                    var lastContactDue = contactDues.LastOrDefault();
                  //  outStanding = total - totalAmtTaken;
                    customerViewModel.OpeningBalance = lastContactDue.Net;
                }


                customerViewModel.Id = contact.Id;
                customerViewModel.FirstName = contact.FirstName;
                customerViewModel.LastName = contact.LastName;
                customerViewModel.Mobile = contact.Mobile;
                customerViewModel.Email = contact.Email;
                customerViewModel.Age = contact.Age;
                customerViewModel.DateOfBirth = contact.DateOfBirth;
                customerViewModel.FathersName = contact.FathersName;
                viewModelList.Add(customerViewModel);
            }

            return Json(viewModelList.Select(m => new
            {
                id = m.Id,
                value = m.FullName,
                label = String.Format("{0} {1} :{2}", m.FirstName, m.LastName, m.Email),
                LastName = m.LastName,
                FullName = m.FullName,
                Age = m.Age,
                DateOfBirth = m.DateOfBirth,
                FathersName = m.FathersName,
                Email = m.Email,
                Mobile = m.Mobile,
                OutStanding = m.OpeningBalance
            }), JsonRequestBehavior.AllowGet);
        }
    }
}
