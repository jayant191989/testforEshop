using HR_Management.Context;
using HR_Management.Models;
using HR_Management.ShoppingCartBL;
using HR_Management.ShoppingCartBL.ViewModels;
using HR_Management.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.Controllers
{
    public class MyAccountController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderHistory()
        {
            return View();
        }

        public ActionResult MyAddress()
        {
            UserAddressesList userAddressesList = new UserAddressesList();
            List<UserAddressViewModel> userAddressViewModelList = new List<UserAddressViewModel>();
            CheckOutBL checkOutBL = new CheckOutBL();
            userAddressViewModelList = checkOutBL.GetAddressList(User.Identity.Name);
            userAddressesList.UserAddressViewModelList = userAddressViewModelList;
            return View(userAddressesList);
        }

        public ActionResult AddNewAddress()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewAddress(UserAddressViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    UserAddress userAddress = new UserAddress();
                    userAddress.Id = Guid.NewGuid();
                    userAddress.FirstName = viewModel.FirstName;
                    userAddress.LastName = viewModel.LastName;
                    userAddress.Company = viewModel.Company;
                    userAddress.AddressLine1 = viewModel.AddressLine1;
                    userAddress.AddressLine2 = viewModel.AddressLine2;
                    userAddress.NearestLandMark = viewModel.NearestLandMark;
                    userAddress.Mobile = viewModel.Mobile;
                    userAddress.City = viewModel.City;
                    userAddress.State = viewModel.State;
                    userAddress.Country = viewModel.Country;
                    userAddress.ZipCode = viewModel.ZipCode;
                    userAddress.AddressTitle = viewModel.AddressTitle;
                    userAddress.ApplicationUserEmail = User.Identity.Name;
                    db.UserAddresses.Add(userAddress);
                    db.SaveChanges();
                    return RedirectToAction("MyAddress");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(viewModel);
        }

        public ActionResult EditAddress(Guid id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                UserAddress userAddress = db.UserAddresses.Find(id);
                if (userAddress == null)
                {
                    return HttpNotFound();
                }
                UserAddressViewModel viewModel = new UserAddressViewModel();
                viewModel.Id = Guid.NewGuid();
                viewModel.FirstName = userAddress.FirstName;
                viewModel.LastName = userAddress.LastName;
                viewModel.AddressLine1 = userAddress.AddressLine1;
                viewModel.AddressLine2 = userAddress.AddressLine2;
                viewModel.NearestLandMark = userAddress.NearestLandMark;
                viewModel.Mobile = userAddress.Mobile;
                viewModel.City = userAddress.City;
                viewModel.State = userAddress.State;
                viewModel.Country = userAddress.Country;
                viewModel.ZipCode = userAddress.ZipCode;
                viewModel.AddressTitle = userAddress.AddressTitle;
                viewModel.ApplicationUserId = User.Identity.Name;
                return View(viewModel);
            }

        }

        public ActionResult MyAccountDetails()
        {
            return View();
        }
        public ActionResult MyWishList()
        {
            return View();
        }

    }
}